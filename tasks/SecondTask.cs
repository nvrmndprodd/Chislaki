namespace tasks;

using static System.Console;

public class SecondTask
{
    private int n = 3;

    private double[][] _mainMatrix;
    private double[] _rightMatrix;

    public void Execute()
    {
        #region input

        WriteLine("Введите размеры матрицы A");
        WriteLine("n = ");

        n = int.Parse(ReadLine());

        _rightMatrix = new double[n];
        _mainMatrix = new double[n][];
        for (var i = 0; i < n; i++)
            _mainMatrix[i] = new double[n];

        for (var i = 0; i < n; i++)
        {
            _rightMatrix[i] = 0;
            for (var j = 0; j < n; j++)
                _mainMatrix[i][j] = 0;
        }

        WriteLine("Введите матрицу A");

        for (var i = 0; i < n; ++i)
        {
            var s = ReadLine();
            while (string.IsNullOrEmpty(s))
                s = ReadLine();
            var input = s.Split(" ");

            for (var j = 0; j < n; ++j)
                _mainMatrix[i][j] = double.Parse(input[j]);
        }

        WriteLine("Введите матрицу B");

        for (var i = 0; i < n; ++i)
            _rightMatrix[i] = double.Parse(ReadLine());

        #endregion

        var gauss = new Gauss(_mainMatrix, _rightMatrix, n);
        gauss.Execute();

        var seidel = new Seidel(n, _mainMatrix, _rightMatrix);
        seidel.Execute();
    }
}