using System;
using System.Reflection;
using Hydra.Core.Enums;
using System.Linq.Expressions;
using Hydra.Core.WorkFlow.Workers;

namespace Hydra.Core.Definitions.Builders
{
    public static class ProcessorDefintionBuilder
    {
        private static ProcessorDefinition BuildStaticProcessorDefinition<TInput,TOutput>(MethodInfo staticMethod, bool locking, string name)
        {
            //Build call lambda
            ParameterExpression x = Expression.Parameter(typeof(TInput),"x");
            MethodCallExpression call = Expression.Call(staticMethod, x);
            Expression<Func<TInput,TOutput>> packagedCall = Expression.Lambda<Func<TInput,TOutput>>(call, new[] { x });
            
            Func<IWorkProcessor<TInput,TOutput>> caller;
            if (locking)
            {
                //Build new LockingSimpleWorker.
                LockingSimpleWorker<TInput,TOutput> w = new LockingSimpleWorker<TInput,TOutput>(packagedCall.Compile());
                //Build expression to return that instance.
                caller = ()=> w;
            }
            else
            {
                //Build new LockingSimpleWorker.
                SimpleWorker<TInput,TOutput> w = new SimpleWorker<TInput,TOutput>(packagedCall.Compile());
                //Build expression to return that instance.
                caller = ()=> w;
            }

            return new ProcessorDefinitionImp<TInput, TOutput>(caller) { OperationName = name };
        }

        //TODO: Build instanced processor capability
        private static ProcessorDefinition BuildInstancedProcessorDefinition(Type containerType, InstanceType instanceType, MethodInfo instanceMethod)
        {
            throw new NotImplementedException();
        }
    
        public static ProcessorDefinition BuildProcessor(string name, MethodInfo seedMethod ,InstanceType instanceType)
        {
            Type returnType = seedMethod.ReturnType;
            Type inputType = seedMethod.GetParameters()[0].ParameterType;

            Type t = typeof(ProcessorDefintionBuilder);
            MethodInfo gm;
            MethodInfo cm;
            switch (instanceType)
            {
                case InstanceType.InstancePerCall:
                    throw new NotImplementedException();                    
                case InstanceType.SingleInstanceParrallelSafe:
                    throw new NotImplementedException();                  
                case InstanceType.SingleInstanceLockRequired:
                   throw new NotImplementedException();                   
                case InstanceType.StaticParrallelSafe:
                   gm = t.GetMethod("BuildStaticProcessorDefinition", BindingFlags.NonPublic | BindingFlags.Static);
                   cm = gm.MakeGenericMethod(new[] {inputType,returnType});
                   return (ProcessorDefinition)cm.Invoke(null, new object[] { seedMethod, false,name });                
                case InstanceType.StaticLockRequired:
                   gm = t.GetMethod("BuildStaticProcessorDefinition", BindingFlags.NonPublic | BindingFlags.Static);
                   cm = gm.MakeGenericMethod(new[] { inputType, returnType });
                   return (ProcessorDefinition)cm.Invoke(null, new object[] { seedMethod, true,name }); 
                default:
                    throw new NotImplementedException();
            }
        }


    }
}
