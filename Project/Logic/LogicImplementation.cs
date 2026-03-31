using System;
using System.Collections.Generic;
using Project.Data;

namespace Project.Logic
{
    internal class LogicImplementation : LogicAbstractAPI
    {
        private List<IBall> ListOfBalls;
        DataAbstractAPI Data;
        public LogicImplementation (DataAbstractAPI data)
        {
            Data = data;
        }
        public override void Start()
        {
            Data.Load();
        }
    }
}