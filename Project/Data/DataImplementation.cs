using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Project.Data
{
    internal class DataImplementation : DataAbstractAPI
    {
        required public IVector XPositionRange { get; set; }
        required public IVector YPositionRange { get; set; }
        required public IVector XVelocityRange { get; set; }
        required public IVector YVelocityRange { get; set; }
        required public IVector MassRange { get; set; }
        required public IVector DiameterRange { get; set; }
        required public string DiagnosticFileName { get; set; }

        private readonly List<IBall> listOfBalls = new List<IBall>();

        private FileStream? diagnosticStream;
        private Thread? diagnosticThread;
        private CancellationTokenSource? cancelSource;
        private ConcurrentQueue<string> diagnosticEntries = new ConcurrentQueue<string>();

        private bool disposed = false;

        public DataImplementation () {}

        public override bool StartDiagnostics()
        {
            if (IsDiagnosticsStarted())
            {
                return false;
            }
            
            if (diagnosticStream != null)
            {
                diagnosticStream.DisposeAsync().AsTask().ContinueWith(t => Console.WriteLine(t.Exception),
                    System.Threading.Tasks.TaskContinuationOptions.OnlyOnFaulted);
            }

            try
            {
                diagnosticStream = File.Create(DiagnosticFileName, 4096);
            }
            catch (Exception e) when (
                e is UnauthorizedAccessException ||
                e is IOException ||
                e is ArgumentException ||
                e is NotSupportedException
            )
            {
                Console.WriteLine(e);
                return false;
            }

            cancelSource = new CancellationTokenSource();
            diagnosticThread = new Thread(new ThreadStart(
                () => DiagnosticLoop(cancelSource.Token)));
            diagnosticThread.Start();
            return true;
        }

        public override bool IsDiagnosticsStarted()
        {
            return diagnosticThread != null && diagnosticThread.IsAlive;
        }

        private void DiagnosticLoop(CancellationToken cancelToken)
        {
            while(true)
            {
                if (cancelToken.IsCancellationRequested || diagnosticStream == null || !diagnosticStream.CanWrite)
                {
                    return;
                }
                string? entry;
                if (diagnosticEntries.TryDequeue(out entry))
                {
                    diagnosticStream.Write(Encoding.UTF8.GetBytes(entry));
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
        public override void AddBall(IVector initialPosition, IVector initialVelocity, double mass, double diameter)
        {
            Ball ball = new(initialPosition, initialVelocity, mass, diameter);
            listOfBalls.Add(ball);
            RaiseBallAddedNotification(ball);
        }

        public override void ClearBalls()
        {
            listOfBalls.Clear();
        }

        public override List<IBall> GetBalls()
        {
            return new List<IBall>(listOfBalls);
        }

        private double GetRandomInRange(double rangeStart, double rangeEnd)
        {
            Random random = new Random();
            return rangeStart + (rangeEnd - rangeStart) * random.NextDouble();
        }

        public override void Load(int count)
        {
            for (int i = 0; i < count; i++)
            {
                double valuePosX = GetRandomInRange(XPositionRange.X, XPositionRange.Y);
                double valuePosY = GetRandomInRange(YPositionRange.X, YPositionRange.Y);
                double valueVelX = GetRandomInRange(XVelocityRange.X, XVelocityRange.Y);
                double valueVelY = GetRandomInRange(YVelocityRange.X, YVelocityRange.Y);
                double mass = GetRandomInRange(MassRange.X, MassRange.Y);
                double diameter = GetRandomInRange(DiameterRange.X, DiameterRange.Y);

                Vector pos = new(valuePosX, valuePosY);
                Vector vel = new(valueVelX, valueVelY);

                AddBall(pos, vel, mass, diameter);
            }
        }

        public override void Save() {}
        
        public override void WriteDiagnostic(string text)
        {
            diagnosticEntries.Enqueue(text + Environment.NewLine);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                cancelSource?.Cancel();
                diagnosticStream?.Dispose();
                disposed = true;
            }
        }
    }
}