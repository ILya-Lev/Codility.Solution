namespace ClassicalProblems;

public class Statistics
{
    private readonly IReadOnlyList<double> _sequence;

    public Statistics(IEnumerable<double> sequence) => _sequence = sequence.ToArray();

    public double SumUp() => _sequence.Sum();
    public double GetMean() => _sequence.Average();
    public double GetMedian()
    {
        var ascending = _sequence.OrderBy(v => v).ToArray();
        if (_sequence.Count % 2 != 0) 
            return ascending[_sequence.Count / 2];
        return (ascending[_sequence.Count / 2 - 1] + ascending[_sequence.Count / 2]) / 2;

    }

    /// <summary>
    /// calculates population dispersion; i.e. in average calculation uses denominator = N
    /// </summary>
    public double GetVariance()
    {
        var mean = GetMean();
        return _sequence.Select(x => Math.Pow(x - mean, 2)).Average();
    }

    /// <summary>
    /// calculates selection dispersion (part of the population); i.e. in average calculation uses denominator = N-1
    /// </summary>
    /// <returns>0.0 if teh sequence contains 1 or less items; as N-1 = 0 and we cannot divide</returns>
    public double GetSelectionVariance()
    {
        if(_sequence.Count <= 1) return 0.0;

        var mean = GetMean();
        return _sequence.Select(x => Math.Pow(x - mean, 2)).Sum() / (_sequence.Count - 1);
    }

    public double GetStandardDeviation() => Math.Sqrt(GetVariance());

    /// <summary>
    /// z-score = (x-mean)/std
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<double> GetZScoredSequence()
    {
        var std = GetStandardDeviation();
        if (std == 0)
            return Enumerable.Repeat(0.0, _sequence.Count).ToArray();

        var mean = GetMean();
        return _sequence.Select(x => (x - mean) / std).ToArray();
    }

    public double GetMin() => _sequence.Min();
    public double GetMax() => _sequence.Max();
}

public class DataPoint
{
    private readonly List<double> _dimensions;
    public int DimensionsCount => Dimensions.Count;
    public IReadOnlyList<double> Dimensions => _dimensions;

    public IReadOnlyList<double> Originals { get; }

    public DataPoint(IEnumerable<double> initials)
    {
        Originals = initials.ToArray();
        _dimensions = Originals.ToList();//deep copy
    }

    public void RewriteDimensions(IEnumerable<double> dimensions)
    {
        _dimensions.Clear();
        _dimensions.AddRange(dimensions);
    }

    public double GetDistance(DataPoint other)
    {
        if (other.Originals.Count != Originals.Count)
            throw new ArgumentException($"Expected amount of points is {Originals.Count}, provided is {other?.Originals.Count}");

        //NOTE: there was a bug - distance should be calculated on Dimensions (z-scored based values) not based on the Originals!
        var distances = Dimensions.Zip(other.Dimensions, (lhs, rhs) => Math.Pow(lhs - rhs, 2)).Sum();
        return Math.Sqrt(distances);
    }

    public override string ToString() => string.Join(", ", Originals.Select(p => $"{p}"));
}

public class GovernorPoint : DataPoint
{
    public int Age { get; }
    public string StateName { get; }
    public double Longitude { get; }

    public GovernorPoint(double longitude, int age, string stateName) : base(new []{longitude, age})
    {
        Longitude = longitude;
        Age = age;
        StateName = stateName;
    }

    public override string ToString() => $"{StateName}: (longitude: {Longitude}, age: {Age})";
}

public class Album : DataPoint
{
    public string Name { get; }
    public int Year { get; }
    public double Length { get; }
    public int Tracks { get; }

    public Album(string name, int year, double length, int tracks) : base(new []{length, tracks})
    {
        Name = name;
        Year = year;
        Length = length;
        Tracks = tracks;
    }

    public override string ToString() => $"({Name}, {Year})";
}

public class KMeans<Point> where Point : DataPoint
{
    private readonly Random _random = new(DateTime.UtcNow.Millisecond);

    private readonly IReadOnlyList<Point> _points;
    private readonly List<Cluster> _clusters;

    public IReadOnlyList<DataPoint> Centroids => _clusters.Select(c => c.Centroid).ToArray();

    public KMeans(int k, IEnumerable<Point> points)
    {
        if (k < 1) throw new ArgumentException($"Cluster's count has to be above zero; provided {k}");

        _points = points.ToArray();

        ZScoreNormalize();

        _clusters = Enumerable.Range(1, k)
            .Select(_ => CreateRandomPoint())
            .Select(centroid => new Cluster(centroid))
            .ToList();
    }

    public KMeans(IReadOnlyCollection<DataPoint> centroids, IEnumerable<Point> points)
    {
        if (centroids.Count < 1)
            throw new ArgumentException($"Cluster's count has to be above zero; provided {centroids.Count}");

        _points = points.ToArray();

        ZScoreNormalize();

        _clusters = centroids
            .Select(centroid => new Cluster(centroid))
            .ToList();
    }

    public IReadOnlyList<Cluster> Run(int maxIterations)
    {
        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            foreach (var cluster in _clusters)
                cluster.Points.Clear();

            AssignPointsToClusters();
            var previous = _clusters.Select(c => c.Centroid).ToArray();
            RedefineCentroids();
            var current = _clusters.Select(c => c.Centroid).ToArray();
            if (AreSetsEqual(previous, current))
                break;
        }

        return _clusters;
    }

    private void ZScoreNormalize()
    {
        var zScores = _points.Select(_ => new List<double>()).ToArray();

        //instead of using a value in a DataPoint, use its z-score, but not in values from the point
        //but in perpendicular values set (so called slice)
        for (var dimension = 0; dimension < _points[0].DimensionsCount; dimension++)
        {
            var slice = GetDimensionSlice(_points, dimension);
            var stat = new Statistics(slice);
            var zScoredSlice = stat.GetZScoredSequence();

            for (int pointIndex = 0; pointIndex < zScoredSlice.Count; pointIndex++)
                zScores[pointIndex].Add(zScoredSlice[pointIndex]);
        }

        //assign z-scores from collector (zScores variable) back into each point's dimension property
        for (int pointIndex = 0; pointIndex < zScores.Length; pointIndex++)
            _points[pointIndex].RewriteDimensions(zScores[pointIndex]);
    }

    private DataPoint CreateRandomPoint()
    {
        var randomPointValues = new List<double>();
        for (int dimension = 0; dimension < _points[0].DimensionsCount; dimension++)
        {
            var slice = GetDimensionSlice(_points, dimension);
            var stat = new Statistics(slice);
            
            var min = stat.GetMin();
            var max = stat.GetMax();
            var rand = _random.NextDouble();

            var value = rand * (max - min) + min;
            randomPointValues.Add(value);
        }

        return new DataPoint(randomPointValues);
    }

    private IReadOnlyList<double> GetDimensionSlice(IEnumerable<Point> points, int dimension) => 
        points.Select(p => p.Dimensions[dimension]).ToArray();

    private void AssignPointsToClusters()
    {
        foreach (var point in _points)
        {
            var closestCluster = _clusters[0];
            var minDistance = point.GetDistance(closestCluster.Centroid);
            foreach (var cluster in _clusters)
            {
                var currentDistance = point.GetDistance(cluster.Centroid);
                if (currentDistance <= minDistance)
                {
                    minDistance = currentDistance;
                    closestCluster = cluster;
                }
            }
            closestCluster.Points.Add(point);
        }
    }

    private void RedefineCentroids()
    {
        foreach (var cluster in _clusters)
        {
            if (!cluster.Points.Any()) continue;

            var centroidCoordinates = new List<double>();
            
            //calculate average for the cluster's slice
            for (int dim = 0; dim < cluster.Points[0].DimensionsCount; dim++)
            {
                var slice = GetDimensionSlice(cluster.Points, dim);
                var coordinate = GenerateCentroidDimension(slice);
                centroidCoordinates.Add(coordinate);
            }

            cluster.Centroid = new DataPoint(centroidCoordinates);
        }
    }

    protected virtual double GenerateCentroidDimension(IReadOnlyList<double> slice)
    {
        var sliceMean = slice.Average();
        return Math.Abs(sliceMean) < 1e-3
            ? 0.0
            : sliceMean; //by z-score def will be 0 when all points are in the same cluster
    }

    private bool AreSetsEqual(IReadOnlyCollection<DataPoint> lhs
        , IReadOnlyCollection<DataPoint> rhs)
        => lhs.Count == rhs.Count
           && lhs
               .Zip(rhs, (l, r) => l.Dimensions
                   .Zip(r.Dimensions, (v1, v2) => v1.Equals(v2))
                   .All(areSame => areSame))
               .All(areSame => areSame);

    public class Cluster
    {
        public List<Point> Points { get; } = new List<Point>();
        public DataPoint Centroid { get; set; }

        public Cluster(DataPoint initialCentroid)
        {
            Centroid = initialCentroid
                       ?? throw new ArgumentNullException(nameof(initialCentroid), "Please provide non null centroid");
        }
    }
}

public class KMedians<Point> : KMeans<Point> where Point : DataPoint
{
    public KMedians(int k, IEnumerable<Point> points) : base(k, points)
    {
    }

    public KMedians(IReadOnlyCollection<DataPoint> centroids, IEnumerable<Point> points) : base(centroids, points)
    {
    }

    protected virtual double GenerateCentroidDimension(IReadOnlyList<double> slice)
    {
        var stat = new Statistics(slice);
        var median = stat.GetMedian();
        return Math.Abs(median) < 1e-3 ? 0.0 : median;
    }
}