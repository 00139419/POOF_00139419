using System;

namespace Parcial_3.Controlador
{
    public class NotDepartmentFoundException : Exception
    {
        public NotDepartmentFoundException(string Message) : base(Message) { }
    }
}