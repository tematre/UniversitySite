using DAL;
using DALContracts;
using Ninject;

namespace NinjectInitializer
{
    public static class Initializer
    {

        public static void Init(IKernel kernel)
        {
            var service = new UniversityRepository();
            kernel.Bind<IProfessorRepository>().ToConstant(service);
            kernel.Bind<IStudentRepository>().ToConstant(service);
            kernel.Bind<ISubjectRepository>().ToConstant(service);
        }
    }
}
