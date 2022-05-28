namespace tasks;

using static System.Console;

// z(x) = sh[sqrt(1 + x^2) / (1 - x)] / sin(x^2+0.4)
// x = 0.2(0.01)0.3

public class FirstTask
{
    public void Execute()
    {
        for (var x = 0.2d; x <= 0.3d; x += 0.01d)
        {
            WriteLine("_____________________________________________________________________");
            WriteLine(x);
            WriteLine($"Phi(x) = {Phi(x)}");
            WriteLine($"Delta phi = {1e-6 / 1.128725}");
            WriteLine($"phi*(x) = {Math.Sqrt(1 + x * x) / (1 - x)}");
            WriteLine($"Delta* phi = {Math.Abs(Phi(x) - (Math.Sqrt(1 + x * x) / (1 - x)))}");

            WriteLine($"u(x) = {U(Phi(x))}");
            WriteLine($"Delta u = {1e-6 / 0.941252}");
            WriteLine($"u*(x) = {Math.Sinh(Math.Sqrt(1 + x * x) / (1 - x))}");
            WriteLine($"Delta* u = {Math.Abs(U(Phi(x)) - Math.Sinh(Math.Sqrt(1 + x * x) / (1 - x)))}");

            WriteLine($"v(x) = {V(x)}");
            WriteLine($"Delta v = {1e-6 / 8.963792}");
            WriteLine($"v*(x) = {Math.Sin(x * x + 0.4d)}");
            WriteLine($"Delta* v = {Math.Abs(V(x) - Math.Sin(x * x + 0.4d))}");

            WriteLine($"z(x) = {U(Phi(x)) / V(x)}");
            WriteLine($"Delta z = {1e-6}");
            WriteLine($"z*(x) = {Math.Sinh(Math.Sqrt(1 + x * x) / (1 - x)) / Math.Sin(x * x + 0.4d)}");
            WriteLine($"Delta* z = {Math.Abs(U(Phi(x)) / V(x) - Math.Sinh(Math.Sqrt(1 + x * x) / (1 - x)) / Math.Sin(x * x + 0.4d))}");
        }
    }

    private double Phi(double x)
    {
        var n = 0;
        double phi = 1;
        double phi0;

        do
        {
            n += 2;
            phi0 = Math.Pow(-1, n / 2d) * Math.Pow(x, n) * Math.Pow(-0.5d, n / 2d) / Factorial(n / 2d);
            phi += phi0;
        } while (Math.Abs(phi0) > 1e-6 / 17.701977);

        return phi / (1 - x);
    }

    private double U(double phi)
    {
        var n = 1;
        
        var u = 0d;
        double a;
        
        do
        {
            a = Math.Pow(phi, n) / Factorial(n);
            u += a;
            n += 2;
        } while (Math.Abs(a) > 1e-6 / 4.695502);

        return u;
    }

    private double V(double x)
    {
        var n = 0;

        var v = 0d;
        double a;

        do
        {
            a = Math.Pow(-1, n) * Math.Pow(x * x + 0.4d, 2 * n + 1) / Factorial(2 * n + 1);
            v += a;
            ++n;
        } while (Math.Abs(a) > 1e-6 / 4.218592);

        return v;
    }

    private double Factorial(double x)
    {
        var factorial = 1d;
        for (var k = 1d; k <= x; ++k) 
            factorial *= k;
        return factorial;
    }
}