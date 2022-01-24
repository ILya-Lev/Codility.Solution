using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ClassicalProblems;

public class NeuralNetwork
{
    public Layer[] Layers { get; }

    public NeuralNetwork(int[] neuronsPerLayer
        , double learningRate
        , Func<double, double> activation
        , Func<double, double> derivativeActivation)
    {
        if (neuronsPerLayer.Length < 3)
            throw new ArgumentException($"Should have at least 3 layers," +
                                        $" provided {neuronsPerLayer.Length}");

        var layers = new List<Layer>();
        for (int i = 0; i < neuronsPerLayer.Length; i++)
        {
            var previous = i > 0 ? layers[i - 1] : null;
            var layer = new Layer(previous, neuronsPerLayer[i], learningRate
                                , activation, derivativeActivation);
            layers.Add(layer);
        }

        Layers = layers.ToArray();
    }

    public void Train(double[][] inputs, double[][] expectations)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            var input = inputs[i];
            var expected = expectations[i];

            GetOutputs(input);
            Backpropagate(expected);
            UpdateWeights();
        }
    }

    public Results Validate<T>(double[][] inputs, T[] expected, Func<double[], T> interpret)
    {
        var correctCounter = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            var outputs = GetOutputs(inputs[i]);
            var current = interpret(outputs);
            if (current!.Equals(expected[i]))
                correctCounter++;
        }
        return new Results
        {
            Correct = correctCounter,
            Trials = inputs.Length,
        };
    }

    private double[] GetOutputs(double[] inputs)
    {
        var result = inputs;
        foreach (var layer in Layers)
        {
            result = layer.GetOutputs(result);
        }
        return result;
    }

    private void Backpropagate(double[] expectedResult)
    {
        Layers.Last().AssignOutputLayerDeltas(expectedResult);
        //as last one is already processed
        for (int i = Layers.Length - 2; i >= 0; i--)
        {
            Layers[i].AssignHiddenLayerDeltas(Layers[i + 1]);
        }
    }

    private void UpdateWeights()
    {
        foreach (var layer in Layers.Skip(1))
            foreach (var neuron in layer.Neurons)
                for (int w = 0; w < neuron.Weights!.Length; w++)
                {
                    neuron.Weights[w] += neuron.LearningRate
                                         * layer.Previous!.OutputCache[w]
                                         * neuron.Delta;
                }
    }
    
    public class Results
    {
        private double? _percentage;
        public int Correct { get; init; }
        public int Trials { get; init; }

        public double Percentage => _percentage ??= Correct * 1.0 / Trials;
    }

    public class Layer
    {
        private static readonly Random _random = new(DateTime.UtcNow.Millisecond);
        public Layer? Previous { get; }
        public double[] OutputCache { get; private set; }
        public Neuron[] Neurons { get; }

        public Layer(Layer? previous
            , int neuronsNumber
            , double learningRate
            , Func<double, double> activation
            , Func<double, double> derivativeActivation)
        {
            Previous = previous;
            OutputCache = new double[neuronsNumber];
            Neurons = new Neuron[neuronsNumber];
            for (int i = 0; i < neuronsNumber; i++)
            {
                var weights = Previous is null
                    ? null
                    : Enumerable.Range(1, Previous.Neurons.Length)
                        .Select(_ => _random.NextDouble())
                        .ToArray();
                Neurons[i] = new Neuron(learningRate, activation, derivativeActivation, weights);
            }
        }

        public double[] GetOutputs(double[] inputs)
        {
            OutputCache = Previous is null
                ? inputs
                : Neurons.Select(n => n.GetOutput(inputs)).ToArray();

            return OutputCache;
        }

        public void AssignOutputLayerDeltas(double[] expectedResult) =>
            AssignDeltas(i => GetOutputLayerError(expectedResult, i));

        public void AssignHiddenLayerDeltas(Layer nextLayer) =>
            AssignDeltas(i => GetHiddenLayerError(nextLayer, i));

        private void AssignDeltas(Func<int, double> errorFunction)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                var error = errorFunction(i);

                Neurons[i].Delta = Neurons[i].DerivativeActivationFunction(Neurons[i].OutputCache)
                                   * error;
            }
        }

        private double GetOutputLayerError(double[] expected, int i) => expected[i] - OutputCache[i];

        private static double GetHiddenLayerError(Layer nextLayer, int i)
        {
            //weights for given neuron from each of next layer neurons
            //n.Weights aren't null as next layer is guaranteed to have previous one (which is this)
            var weights = nextLayer.Neurons.Select(n => n.Weights![i]).ToArray();

            var deltas = nextLayer.Neurons.Select(n => n.Delta).ToArray();

            return Utils.GetDotProduct(weights, deltas);
        }
    }

    public class Neuron
    {
        public double LearningRate { get; }
        public Func<double, double> ActivationFunction { get; }
        public Func<double, double> DerivativeActivationFunction { get; }

        //could setters be private
        public double[]? Weights { get; set; }
        public double Delta { get; set; }
        public double OutputCache { get; set; }

        public Neuron(double learningRate
            , Func<double, double> activation
            , Func<double, double> derivativeActivation
            , double[]? weights)
        {
            LearningRate = learningRate;
            ActivationFunction = activation;
            DerivativeActivationFunction = derivativeActivation;
            Weights = weights;
        }

        public double GetOutput(double[] inputs)
        {
            if (Weights is null)
                OutputCache = inputs.Sum();
            else
                OutputCache = Utils.GetDotProduct(Weights, inputs);

            return ActivationFunction(OutputCache);
        }
    }

    public static class Utils
    {
        public static double GetDotProduct(IReadOnlyList<double>? lhs, IReadOnlyList<double> rhs)
        {
            if (lhs?.Count != rhs.Count)
                throw new ArgumentException($"lhs and rhs have different length: {lhs?.Count} vs {rhs.Count}");

            var dotProduct = 0.0;
            for (int i = 0; i < lhs.Count; i++)
            {
                dotProduct += lhs[i] * rhs[i];
            }

            return dotProduct;
        }

        public static double GetSigmoidFor(double x) => 1 / (1 + 1 / Math.Exp(x));

        public static double GetSigmoidDerivativeFor(double x)
        {
            var s = GetSigmoidFor(x);
            return s * (1 - s);
        }

        /// <summary>
        /// NormalizeByFeatureScaling
        /// </summary>
        /// <returns>
        /// new = (old - min)/(max - min)
        /// where max and min are calculated per column
        /// </returns>
        public static List<double[]> Rescale(List<double[]> table)
        {
            var rescaled = new List<double[]>(table);
            for (int col = 0; col < table[0].Length; col++)
            {
                var min = rescaled[0][col];
                var max = rescaled[0][col];

                foreach (var row in rescaled)
                {
                    min = Math.Min(min, row[col]);
                    max = Math.Max(min, row[col]);
                }

                var difference = max - min;
                if (difference != 0)    //otherwise all values are the same and there is no need for scaling
                {
                    foreach (var row in rescaled)
                        row[col] = (row[col] - min) / difference;
                }
            }

            return rescaled;
        }

        /// <summary>
        /// loads a CSV file and spits off 
        /// </summary>
        /// <typeparam name="TData">describes file structure</typeparam>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static IReadOnlyList<TData> LoadCSV<TData, TMap>(string fullFileName)
            where TMap : ClassMap<TData>
            where TData : IDoubleData
        {
            using var reader = new StreamReader(fullFileName);
            using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            });
            csvReader.Context.RegisterClassMap<TMap>();

            var data = csvReader.GetRecords<TData>().ToArray();
            return data;
            //return data.Where(d => d is not null).Select(d => d.ToDoubleArray()).ToList();
        }

        public static IReadOnlyList<T> Shuffle<T>(IEnumerable<T> source)
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var result = new List<T>(source);
            var steps = Math.Sqrt(result.Count);
            for (int i = 0; i < steps; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    var lhs = random.Next(0, result.Count);
                    var rhs = random.Next(0, result.Count);
                    (result[lhs], result[rhs]) = (result[rhs], result[lhs]);
                }
            }
            return result;
        }
    }

    public interface IDoubleData
    {
        double[] ToDoubleArray();
    }

    public class Irises : IDoubleData
    {
        private static readonly IReadOnlyDictionary<string, double[]> _classificationMap =
            new Dictionary<string, double[]>(StringComparer.OrdinalIgnoreCase)
            {
                ["Iris-setosa"] = new[] { 1.0, 0, 0 },
                ["Iris-versicolor"] = new[] { 0, 1.0, 0 },
                ["Iris-virginica"] = new[] { 0, 0, 1.0 },
            };

        public double LeafLength { get; set; }
        public double LeafWidth { get; set; }
        public double FlowerLength { get; set; }
        public double FlowerWidth { get; set; }

        public string Name { get; set; }

        public double[] ToDoubleArray() => new[] { LeafLength, LeafWidth, FlowerLength, FlowerWidth };

        public override string ToString() => $"{Name}: {LeafLength}, {LeafWidth}, {FlowerLength}, {FlowerWidth}";

        /// <summary> make neural network output understandable by a human </summary>
        /// <param name="output">has to be an array of 3 numbers, as there are only 3 kinds of irises in the system</param>
        /// <returns>irises kind name</returns>
        public static string InterpretOutput(double[] output) => _classificationMap
            .Select(p => (Name: p.Key, Score: Utils.GetDotProduct(output, p.Value)))
            .MaxBy(tuple => tuple.Score)
            .Name;

        public static double[] GetExpectedOutput(string name) => _classificationMap
            .TryGetValue(name, out var vector)
            ? vector
            : throw new ArgumentException(
                $"Unknown irises kind name {name}, supported are {string.Join(", ", _classificationMap.Select(p => p.Key))}");
    }

    public sealed class IrisesMap : ClassMap<Irises>
    {
        public IrisesMap()
        {
            Map(r => r.LeafLength);
            Map(r => r.LeafWidth);
            Map(r => r.FlowerLength);
            Map(r => r.FlowerWidth);
            Map(r => r.Name);
        }
    }

    public class IrisesDataPoint : DataPoint
    {
        public string Name { get; }
        
        public IrisesDataPoint(Irises r)
        : base(new []{r.LeafLength, r.LeafWidth, r.FlowerLength, r.FlowerWidth})
        {
            Name = r.Name;
        }

        public override string ToString() => $"{Name}: {base.ToString()}";
    }

    public class IrisesClassification
    {
        public Results Classify(IReadOnlyList<Irises> irises)
        {
            var network = new NeuralNetwork(new[] { 4, 6, 3 }
                //4 input neurons, as 4 parameters to take into account (leaf and flower length and width)
                //6 intermediate neurons, as it seems to be enough
                //3 output neurons, as there are only 3 kinds of neurons represented in the system
                , 0.3//why this number?
                , Utils.GetSigmoidFor
                , Utils.GetSigmoidDerivativeFor);

            var shuffled = Utils.Shuffle(irises);
            var trainingSetCount = irises.Count * 9 / 10;
            var trainingSet = shuffled.Take(trainingSetCount).ToArray();

            var trainInputs = ComposeInputs(trainingSet);
            var trainExpectations = ComposeExpectedDoubles(trainingSet);
            for (int i = 0; i < 50; i++)
            {
                network.Train(trainInputs, trainExpectations);
            }

            var checkSet = shuffled.Skip(trainingSetCount).ToArray();
            var checkInputs = ComposeInputs(checkSet);
            var expectedNames = ComposeExpectedNames(checkSet);
            var result = network.Validate(checkInputs, expectedNames, Irises.InterpretOutput);

            return result;
        }

        private double[][] ComposeInputs(IReadOnlyCollection<Irises> irises) => irises.Select(r => r.ToDoubleArray()).ToArray();

        private double[][] ComposeExpectedDoubles(IReadOnlyCollection<Irises> irises) => irises
            .Select(r => Irises.GetExpectedOutput(r.Name)).ToArray();

        private string[] ComposeExpectedNames(IReadOnlyCollection<Irises> irises) => irises.Select(r => r.Name).ToArray();
    }
}