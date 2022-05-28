namespace tasks;

using static System.Console;

public class Gauss
{
    private int n;
    private double[][] _mainMatrix;
    private double[] _rightMatrix;
    private double[] _gaussAnswer;

    public Gauss(double[][] mainMatrix, double[] rightMatrix, int n)
    {
        _mainMatrix = new double[n][];
        _gaussAnswer = new double[n];
        _rightMatrix = new double[n];
        for (var i = 0; i < n; ++i)
        {
            _gaussAnswer[i] = 0;
            _rightMatrix[i] = rightMatrix[i];

            _mainMatrix[i] = new double[n];
            for (var j = 0; j < n; ++j)
                _mainMatrix[i][j] = mainMatrix[i][j];
        }
        this.n = n;
    }

    public void Execute()
    {
        Diagonalise();
        GaussSolve();

        var s = "";
        for (var i = 0; i < n; i++)
        {
            s += "\r\n";
            for (var j = 0; j < n; j++)
            {
                s += _mainMatrix[i][j].ToString("F06") + "\t";
            }

            s += "\t" + _rightMatrix[i].ToString("F06");
            s += "\t" + _gaussAnswer[i].ToString("F06");
        }

        WriteLine(s);
    }

    private void SortRows(int sortIndex)
    {
        double maxElement = _mainMatrix[sortIndex][sortIndex];
        int maxElementIndex = sortIndex;
        for (int i = sortIndex + 1; i < n; i++)
        {
            if (_mainMatrix[i][sortIndex] > maxElement)
            {
                maxElement = _mainMatrix[i][sortIndex];
                maxElementIndex = i;
            }
        }

        //теперь найден максимальный элемент и ставим его на верхнее место
        if (maxElementIndex > sortIndex)
        {
            double Temp;

            Temp = _rightMatrix[maxElementIndex];
            _rightMatrix[maxElementIndex] = _rightMatrix[sortIndex];
            _rightMatrix[sortIndex] = Temp;

            for (int i = 0; i < n; i++)
            {
                Temp = _mainMatrix[maxElementIndex][i];
                _mainMatrix[maxElementIndex][i] = _mainMatrix[sortIndex][i];
                _mainMatrix[sortIndex][i] = Temp;
            }
        }
    }

    private void Diagonalise()
    {
        for (var i = 0; i < n - 1; i++)
        {
            SortRows(i);
            for (var j = i + 1; j < n; ++j)
            {
                if (_mainMatrix[i][i] != 0)
                {
                    var multiplier = _mainMatrix[j][i] / _mainMatrix[i][i];
                    for (var k = 0; k < n; ++k)
                        _mainMatrix[j][k] -= _mainMatrix[i][k] * multiplier;
                    _rightMatrix[j] -= _rightMatrix[i] * multiplier;
                }
            }
        }
    }

    private void GaussSolve()
    {
        for (var i = n - 1; i >= 0; --i)
        {
            _gaussAnswer[i] = _rightMatrix[i];

            for (var j = n - 1; j > i; --j)
                _gaussAnswer[i] -= _mainMatrix[i][j] * _gaussAnswer[j];

            if (_mainMatrix[i][i] == 0)
            {
                if (_rightMatrix[i] == 0)
                {
                    WriteLine("Множество решений");
                    break;
                }

                WriteLine("Нет решений");
                break;
            }

            _gaussAnswer[i] /= _mainMatrix[i][i];
        }
    }
}