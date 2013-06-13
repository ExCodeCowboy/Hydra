using System;

namespace Hydra.Core.Definitions
{
    public class ProcessorDefinitionImp<TInput,TOutput>:ProcessorDefinition
    {
        private Func<IWorkProcessor<TInput, TOutput>> _factoryMethod;
        public ProcessorDefinitionImp(Func<IWorkProcessor<TInput,TOutput>> factoryMethod)
        {
            _factoryMethod = factoryMethod;
            InputType = typeof(TInput);
            OutputType = typeof(TOutput);
        }

        public IWorkProcessor<TInput, TOutput> GetProcessor()
        {
            return _factoryMethod();
        }
    }
}
