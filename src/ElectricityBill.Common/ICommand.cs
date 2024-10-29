using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityBill.Common
{
    public interface ICommand<TResult>
    {
    }

    public interface ICommand : ICommand<Unit>
    {
    }
    public struct Unit
    {
        public static Unit Value => new Unit();
    }


    //public interface ICommand
    //{

    //}
    //public interface ICommandHandler<in TCommand, TResult>
    //{
    //    Task<TResult> HandleAsync(TCommand command);
    //}


    //public interface ICommandHandler<in TCommand>
    //{
    //    Task HandleAsync(TCommand command);
    //}
}
