using System.Drawing;

namespace tasks;

// ctgx = x

public class ThirdTask
{
    private const double Epsilon = 1e-6;

    public void Execute()
    {
        NewtonMethodEquation(0.5d, 2.5d);
        NewtonMethodSystem();
    }

    #region Equation

    private double F(double x) => 
        x - 1 / Math.Tan(x);

    private void NewtonMethodEquation(double a0, double b0)
    {
        var a = a0;
        var b = b0;

        var x = (a + b) / 2;
        double x0;

        double c = 0d;

        int k = 0;

        do
        {
            Console.WriteLine($"x{k} = {x}; a{k} = {a}; b{k} = {b}");
            
            x0 = x;
            x -= F(x) / Derivative(F, x);

            if (x < a || x > b)
                x = (a + b) / 2;

            if (F(a) < 0)
                c = F(x);
            else if (F(a) > 0)
                c = -F(x);

            if (c < 0)
                a = x;
            else if (c > 0)
                b = x;

            ++k;
        } while (Math.Abs(x - x0) > Epsilon);
    }

    private double Sign(double x)
    {
        if (x == 0) return 0;
        return x < 0 ? -1 : 1;
    }

    private double Derivative(Func<double, double> f, double x) => 
        (f(x + Epsilon) - f(x - Epsilon)) / (2 * Epsilon);

    #endregion

    #region System

    private void NewtonMethodSystem()
    {
        double[] x0 = {-0.8, 0.5};
        double[] x = x0;
        
        do
        {
            Console.WriteLine($"({x0[0]}, {x0[1]})");
            
            x0 = x;
            
            var jacobian = Jacobian(x0[0], x0[1]);

            double[] delta = {0, 0};

            double[][] matrix = new double[][]
            {
                new[] {DerivativeX(f1, x0[0], x0[1]), DerivativeY(f1, x0[0], x0[1]), -f1(x0[0], x0[1])},
                new[] {DerivativeX(f2, x0[0], x0[1]), DerivativeY(f2, x0[0], x0[1]), -f2(x0[0], x0[1])}
            };

            var m = matrix[0][0] / matrix[1][0];
            for (var i = 0; i < 3; ++i) 
                matrix[1][i] = matrix[1][i] * m - matrix[0][i];

            delta[1] = matrix[1][2] / matrix[1][1];
            delta[0] = (matrix[0][2] - matrix[0][1] * delta[1]) / matrix[0][0];

            x = new double[] {x0[0] + delta[0], x0[1] + delta[1]};
        } while (Error(x0, x) > Epsilon);

        Console.WriteLine($"Answer is: ({x0[0]}, {x0[1]})");
    }

    private double f1(double x, double y) => 
        2 * y - Cos(x + 1);

    private double f2(double x, double y) =>
        x + Sin(y) + 0.4d;

    private double DerivativeX(Func<double, double, double> f, double x, double y) => 
        (f(x + Epsilon, y) - f(x - Epsilon, y)) / (2 * Epsilon);

    private double DerivativeY(Func<double, double, double> f, double x, double y) => 
        (f(x, y + Epsilon) - f(x, y - Epsilon)) / (2 * Epsilon);

    private double Jacobian(double x, double y) => 
        DerivativeX(f1, x, y) * DerivativeY(f2, x, y) - DerivativeX(f2, x, y) * DerivativeY(f1, x, y);

    private double Sin(double x)
    {
        double u0, u = 0;
        int k = 0;
        while (Math.Abs(x) > Math.PI) {
            if (x > Math.PI)
                x -= 2 * Math.PI;
            else if (x < -Math.PI)
                x += 2 * Math.PI;
        }
        do {
            u0 = Math.Pow(-1, k) * Math.Pow(x, 2 * k + 1) / Fact(2 * k + 1);
            u += u0;
            k++;
        } while (Math.Abs(u0) > 1e-4);
        return u;
    }

    private double Cos(double x)
    {
        double u0, u = 0;
        int k = 0;
        while (Math.Abs(x) > Math.PI) {
            if (x > Math.PI)
                x -= 2 * Math.PI;
            else if (x < -Math.PI)
                x += 2 * Math.PI;
        }
        do {
            u0 = Math.Pow(-1, k) * Math.Pow(x, 2 * k) / Fact(2 * k);
            u += u0;
            k++;
        } while (Math.Abs(u0) > 1e-4);
        return u;
    }

    private double Error(double[] x0, double[] x)
    {
        double error = 0;
        for (var i = 0; i < x0.Length; ++i) 
            error += Math.Pow(Math.Abs(x0[i] - x[i]), 2);

        error = Math.Sqrt(error);
        
        return error;
    }

    double Fact(double k) {
        double fact = 1;
        while (k > 1) {
            fact *= k;
            k -= 1;
        }
        return fact;
    }

    #endregion
}