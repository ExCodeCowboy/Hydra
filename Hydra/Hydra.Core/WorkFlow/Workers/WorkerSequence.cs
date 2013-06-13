namespace Hydra.Core.WorkFlow.Workers
{
    public class WorkerSequence<TInput, TIntermediate,TOutput> : IWorkProcessor<TInput, TOutput>
    {
        private IWorkProcessor<TInput, TIntermediate> _inputStep;
        private IWorkProcessor<TIntermediate, TOutput> _outputStep; 

        public WorkerSequence(IWorkProcessor<TInput, TIntermediate> inputStep,
                              IWorkProcessor<TIntermediate, TOutput> outputStep)
        {
            _inputStep = inputStep;
            _outputStep = outputStep;
        }

        public TOutput Process(TInput workIn)
        {
            return _outputStep.Process(_inputStep.Process(workIn));
        }
    }
}
