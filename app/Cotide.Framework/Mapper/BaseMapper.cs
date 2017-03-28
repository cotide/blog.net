using System;
using System.Collections.Generic;
using System.Text;

namespace Cotide.Framework.Mapper
{
    /// <summary>
    /// 映射视图实现
    /// </summary>
    /// <typeparam name="TInput">映射源</typeparam>
    /// <typeparam name="TOutput">映射目标</typeparam>
    public   class BaseMapper<TInput, TOutput> : IMapper<TInput, TOutput>
    {
        public BaseMapper()
        {
            CreateMap();
        }

        #region IMapper<TInput,TOutput> Members

        public virtual TOutput MapFrom(TInput input)
        {
            return AutoMapper.Mapper.Map<TInput, TOutput>(input);
        }

        #endregion

        protected virtual void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<TInput, TOutput>();
        }
    }

    /// <summary>
    /// 映射视图实现
    /// </summary>
    /// <typeparam name="TInput1"></typeparam>
    /// <typeparam name="TInput2"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public   class BaseMapper<TInput1, TInput2, TOutput> : IMapper<TInput1, TInput2, TOutput>
    {
        public BaseMapper()
        {
            CreateMap();
        }

        #region IMapper<TInput1,TInput2,TOutput> Members

        public virtual TOutput MapFrom(
            TInput1 input1,
            TInput2 input2)
        {
            var result = AutoMapper.Mapper.Map<TInput1, TOutput>(input1);
            AutoMapper.Mapper.Map(input2, result);

            return result;
        }

        #endregion

        protected virtual void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<TInput1, TOutput>();
            AutoMapper.Mapper.CreateMap<TInput2, TOutput>();
        }
    }
}
