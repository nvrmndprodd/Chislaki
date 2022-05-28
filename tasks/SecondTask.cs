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

        Seidel();
    }

    private void Seidel()
    {
        var epsilon = 1e-6;

        var matrix = new double[n][];
        var previousVariableValues = new double[n];

        for (var i = 0; i < n; ++i)
        {
            matrix[i] = new double[n + 1];
            previousVariableValues[i] = 0;
            for (var j = 0; j < n + 1; ++j)
            {
                if (j == n)
                    matrix[i][j] = _rightMatrix[i];
                else
                    matrix[i][j] = _mainMatrix[i][j];
            }
        }

        var counter = 0;

        while (true)
        {
            ++counter;
            // Введем вектор значений неизвестных на текущем шаге       
            var currentVariableValues = new double[n];

            // Посчитаем значения неизвестных на текущей итерации
            // в соответствии с теоретическими формулами
            for (int i = 0; i < n; i++)
            {
                // Инициализируем i-ую неизвестную значением 
                // свободного члена i-ой строки матрицы
                currentVariableValues[i] = matrix[i][n];

                // Вычитаем сумму по всем отличным от i-ой неизвестным
                for (int j = 0; j < n; j++)
                {
                    // При j < i можем использовать уже посчитанные
                    // на этой итерации значения неизвестных
                    if (j < i) 
                        currentVariableValues[i] -= matrix[i][j] * currentVariableValues[j];

                    // При j > i используем значения с прошлой итерации
                    if (j > i) 
                        currentVariableValues[i] -= matrix[i][j] * previousVariableValues[j];
                }

                // Делим на коэффициент при i-ой неизвестной
                currentVariableValues[i] /= matrix[i][i];
            }

            // Посчитаем текущую погрешность относительно предыдущей итерации
            var error = 0.0d;

            for (int i = 0; i < n; i++) 
                error += Math.Abs(currentVariableValues[i] - previousVariableValues[i]);

            // Если необходимая точность достигнута, то завершаем процесс
            if (error < epsilon) break;

            // Переходим к следующей итерации, так 
            // что текущие значения неизвестных 
            // становятся значениями на предыдущей итерации
            
            WriteLine($"Iteration: {counter}");
            for (var j = 0; j < n; ++j)
            {
                WriteLine($"x{j} = {currentVariableValues[j]}");
            }

            WriteLine();
            WriteLine();
            
            previousVariableValues = currentVariableValues;
        }

        // Выводим найденные значения неизвестных с 8 знаками точности
        for (int i = 0; i < n; i++)
        {
            WriteLine(previousVariableValues[i].ToString("F06"));
        }
    }
}