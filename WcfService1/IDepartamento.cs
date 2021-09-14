using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDepartamento" in both code and config file together.
    [ServiceContract]
    public interface IDepartamento
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        Result Add(ML.Departamento departamento);

        [OperationContract]
        Result Update(ML.Departamento departamento);

        [OperationContract]
        Result Delete(int IdDepartamento);

        [OperationContract]
        [ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        Result GetAll();

        [OperationContract]
        [ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        Result GetById(int IdDepartamento);
        
    }

    //[DataContract]
    //public class Result
    //{
    //    [DataMember]
    //    public bool Correct { get; set; }

    //    [DataMember]
    //    public object Object { get; set; }

    //    [DataMember]
    //    public List<object> Objects { get; set; }

    //    [DataMember]
    //    public string ErrorMessage { get; set; }

    //    [DataMember]
    //    public Exception ex { get; set; }
    //}


}
