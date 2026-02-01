using System;
using System.Collections.Generic;
using System.Text;
using org.mariuszgromada.math.mxparser;
namespace Regula_falsi_metodu
{
    
    class Calculate
    {
        public static bool controlFuncSyntax(string f)
        {
            Function f1 = new Function("f(x) = " + f);
            return f1.checkSyntax();
        }
        public static bool controlIntervals(double interval1, double interval2, string function)
        {
            
            Function f = new Function("f(x)=" + function);
            double f1 = f.calculate(interval1);
            double f2 = f.calculate(interval2);
            if (f1 * f2 < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public static List<Result> calcResult(double interval1,double interval2,string func, int acc,int digit)
        {
            
            Function f = new Function("f(x)=" + func);
            List<Result> rList = new List<Result>();
           
            double a = interval1;
            double b = interval2;
            double c=0;
            double cOld = double.MinValue; //initial value of cOld, being too little providing that the loop will work
            string digitFormat = "F" + digit.ToString();

            while (Math.Abs(c - cOld) > Math.Pow(10, -acc)){ 
             cOld = c;
            c = a-(f.calculate(a) * (b - a) / (f.calculate(b) - f.calculate(a)));

            Result r = new Result();
            r.a = a;
            r.b = b;
            r.c = c;
            r.fa = f.calculate(a);
            r.fb = f.calculate(b);
            r.fc = f.calculate(c);

            if (f.calculate(c) < 0)
            {
                if (f.calculate(a) < 0)
                {
                    a = c;
                    r.new_interval = $"f({c.ToString(digitFormat)})={(r.fc).ToString(digitFormat)}<0; yeni aralık [{c:F4},{b:F4}]";
                }else if (f.calculate(b)<0)
                {
                    b = c;
                    r.new_interval = $"f({c.ToString(digitFormat)})={(r.fc).ToString(digitFormat)}<0; yeni aralık [{a:F4},{c:F4}]";
                }
            }else if (f.calculate(c) > 0)
            {
                if (f.calculate(a) > 0)
                {
                    a = c;
                    r.new_interval = $"f({c.ToString(digitFormat)})={(r.fc).ToString(digitFormat)}>0; yeni aralık [{c:F4},{b:F4}]";
                }
                else if (f.calculate(b) > 0)
                {
                    b = c;
                    r.new_interval = $"f({c.ToString(digitFormat)})={(r.fc).ToString(digitFormat)}>0; yeni aralık [{a:F4},{c:F4}]";
                }
            }
               
                rList.Add(r);

            }
            
             return rList; 
            
            
            

            
               
            
        }
    }
}
