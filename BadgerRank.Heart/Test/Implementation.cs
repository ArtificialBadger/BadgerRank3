using System;
using System.Collections.Generic;
using System.Text;

namespace BadgerRank.Heart.Test
{
    public sealed class Implementation : IAbstraction
    {
        private readonly Guid id = Guid.NewGuid();

        public string Gib()
        {
            return this.id.ToString();
        }
    }
}
