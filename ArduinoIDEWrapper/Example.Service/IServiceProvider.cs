using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.IDE
{
    public interface IServiceProvider<T,U>
    {

        /// <summary>
        /// Create an instance of type T.
        /// </summary>
        /// <param name="args">Any arguments needed to create the type</param>
        /// <returns>An non-null instance of T</returns>
        void Create(U args);

        /// <summary>
        /// Returns the instance created by the Create() function.
        /// </summary>
        /// <returns>An non-null instance of T</returns>
        T GetService();

    }
}
