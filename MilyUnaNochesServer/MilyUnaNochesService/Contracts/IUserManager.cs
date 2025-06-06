﻿using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace MilyUnaNochesService.Contracts
{
    [ServiceContract]
    public interface IUserManager
    {
        [OperationContract]
        int AddClient(Usuario usuario);

        [OperationContract]
        int AddEmployee(Usuario user, UserDireccion address, Empleado employee, Acceso acces);
        [OperationContract]
        int ArchiveClient(int idUsuario);

        [OperationContract]
        int ArchiveEmployee(int idUsuario);


        [OperationContract]
        List<Contracts.Usuario> GetUserProfileByNamePhone(string searchTerm);

        [OperationContract]
        List<Contracts.Empleado> GetActiveEmployeesBySearchTerm(string searchTerm);


        [OperationContract]
        bool VerifyAccess(string username, string password);

        [OperationContract]
        int VerifyExistinClient(string name,string firstLastName, string secondLastName);

        [OperationContract]
        int VerifyExistinEmployee(string email, string phone);

        [OperationContract]
        int VerifyExistingAccesAccount(string username);

        [OperationContract]
        int VerifyCredentials(string username, string password);

        [OperationContract]
        string searchEmployeeType(string username, string password);
        [OperationContract]
        Empleado GetUserProfile(string username, string password);

        [OperationContract]
        Empleado GetEmployee(int idUser);

        [OperationContract]
        int UpdateEmployee(Empleado employee);

        [OperationContract]
        Usuario GetClient(int idUser);

        [OperationContract]
        int UpdateClient(Usuario client);

        [OperationContract]
        int GetClienteId(int idUsuario);

    }

    [DataContract]
    public class Usuario
    {
        [DataMember]
        public int idUsuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string primerApellido { get; set; }
        [DataMember]
        public string segundoApellido { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string estadoUsuario { get; set; }

    }



    [DataContract]
    public class Empleado
    {
        [DataMember]
        public int idUsuario { get; set; }
        [DataMember]
        public int idAcceso { get; set; }
        [DataMember]
        public int idEmpleado { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string primerApellido { get; set; }
        [DataMember]
        public string segundoApellido { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string telefono { get; set; }

        [DataMember]
        public string tipoEmpleado { get; set; }

        [DataMember]
        public string calle { get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public string codigoPostal { get; set; }
        [DataMember]
        public string ciudad { get; set; }


    }

    [DataContract]
    public class Acceso
    {
        [DataMember]
        public int idAcceso { get; set; }
        [DataMember]
        public int idEmpleado { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string contraseña { get; set; }
       
    }
    [DataContract]
    public class Cliente
    {
        [DataMember]
        public int idCliente { get; set; }
        [DataMember]
        public int idUsuario { get; set; }

    }

    [DataContract]
    public class UserDireccion
    {
        [DataMember]
        public int idDireccion { get; set; }
        [DataMember]
        public string calle { get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public string codigoPostal { get; set; }
        [DataMember]
        public string ciudad { get; set; }
    }
}
