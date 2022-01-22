namespace ClassicalProblems;

public class NeuralNetwork
{
    public class Network
    {
        public Layer[] Layers { get; }

        public Network(int[] neuronsPerLayer
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
                var previous = i > 0 ? layers[i-1] : null;
                var layer = new Layer(previous, neuronsPerLayer[i], learningRate
                                    , activation, derivativeActivation);
                layers.Add(layer);
            }

            Layers = layers.ToArray();
        }

        public double[] GetOutputs(double[] inputs)
        {
            var result = inputs;
            foreach (var layer in Layers)
            {
                result = layer.GetOutputs(result);
            }
            return result;
        }

        public void Backpropagate(double[] expectedResult)
        {
            Layers.Last().AssignOutputLayerDeltas(expectedResult);
            //as last one is already processed
            for (int i = Layers.Length - 2; i >= 0; i--)
            {
                Layers[i].AssignHiddenLayerDeltas(Layers[i+1]);
            }
        }

        public void UpdateWeights()
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

        public Results Valdate<T>(double[][] inputs, T[] expected, Func<double[], T> interpret)
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
        private static readonly Random _random = new (DateTime.UtcNow.Millisecond);
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
            if (lhs.Count != rhs.Count)
                throw new ArgumentException($"lhs and rhs have different length: {lhs.Count} vs {rhs.Count}");

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
    }
}