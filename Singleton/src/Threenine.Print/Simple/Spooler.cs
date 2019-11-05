
namespace Threenine.Print.Simple
{
    
    /// <summary>
    /// Simple Implementation of the Singleton Pattern.
    /// Not Thread safe and not good practice
    /// DO NOT USE
    /// </summary>
   public sealed class Spooler : Spool
   {
       private static Spooler _instance;
   
     
       public static Spooler Instance => _instance ??= new Spooler();
       
   }
}