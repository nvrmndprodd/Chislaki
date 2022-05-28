namespace tasks;

using static System.Console;

public class Seidel
{
    private const double Epsilon = 1e-6;
    
    private int n;

    private double[][] _mainMatrix;

    public Seidel(int n, double[][] mainMatrix, double[] rightMatrix)
    {
        this.n = n;
        _mainMatrix = new double[n][];
        
        for (var i = 0; i < n; ++i)
        {
            _mainMatrix[i] = new double[n + 1];
            for (var j = 0; j < n + 1; ++j)
            {
                if (j == n)
                    _mainMatrix[i][j] = rightMatrix[i];
                else
                    _mainMatrix[i][j] = mainMatrix[i][j];
            }
        }
    }

    public void Execute()
    {
        if (!DiagonalDominance()) return;
        
        var previousVariableValues = new double[n];

        for (var i = 0; i < n; ++i) 
            previousVariableValues[i] = 0;

        var counter = 0;

        while (true)
        {
            ++counter;
            // Введем вектор значений неизвестных на текущем шаге       
            var currentVariableValues = new double[n];

            for (int i = 0; i < n; i++)
            {
                // Инициализируем i-ую неизвестную значением 
                // свободного члена i-ой строки матрицы
                currentVariableValues[i] = _mainMatrix[i][n];

                // Вычитаем сумму по всем отличным от i-ой неизвестным
                for (int j = 0; j < n; j++)
                {
                    // При j < i можем использовать уже посчитанные
                    // на этой итерации значения неизвестных
                    if (j < i) 
                        currentVariableValues[i] -= _mainMatrix[i][j] * currentVariableValues[j];

                    // При j > i используем значения с прошлой итерации
                    if (j > i) 
                        currentVariableValues[i] -= _mainMatrix[i][j] * previousVariableValues[j];
                }

                // Делим на коэффициент при i-ой неизвестной
                currentVariableValues[i] /= _mainMatrix[i][i];
            }


            WriteLine($"Iteration: {counter}");
            for (var j = 0; j < n; ++j)
            {
                WriteLine($"x{j} = {currentVariableValues[j]}");
            }


            WriteLine();
            WriteLine();

            // Посчитаем текущую погрешность относительно предыдущей итерации
            var error = 0.0d;

            for (int i = 0; i < n; i++) 
                error += Math.Abs(currentVariableValues[i] - previousVariableValues[i]);
            if (error < Epsilon) break;
            
            previousVariableValues = currentVariableValues;
        }

        for (int i = 0; i < n; i++)
        {
            WriteLine(previousVariableValues[i].ToString("F06"));
        }
    }

    private bool DiagonalDominance()
    {
        for (var i = 0; i < n; ++i)
        {
            var sum = 0d;
            for (var j = 0; j < n; ++j)
            {
                if (j != i)
                    sum += _mainMatrix[i][j];
            }

            if (Math.Abs(_mainMatrix[i][i] - sum) < 0)
                throw new ArgumentException("Метод Зейделя: матрица не обладает свойством диагонального преобладания");
        }

        return true;
    }
}