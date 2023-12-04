using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static bool Login(ML.Usuario usuario)
        {
            bool result = false;
            using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
            {
                var query = context.Usuarios.FromSqlRaw($"UsuarioAcceder '{usuario.UserName}',@Password", new SqlParameter("@Password", usuario.Password)).AsEnumerable().FirstOrDefault();
                if (query != null)
                {
                    result = true;
                }
            }
            return result;
        }
        public static bool Add(ML.Usuario usuario)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}',@Password", new SqlParameter("@Password",usuario.Password));
                    if(rowsAffected > 0)
                    {
                        correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
            }
            return correct;
        }
    }
}
