using System;
using System.Collections.Generic;
using System.Text;

namespace Cotide.Framework.Mapper
{
    /// <summary>
    /// 映射实体类型接口
    /// </summary>
    /// <typeparam name="TInput">映射源</typeparam>
    /// <typeparam name="TOutput">映射目标</typeparam>
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput MapFrom(TInput input);
    }

    /// <summary>
    /// 映射实体类型接口（扩展）
    /// </summary>
    /// <typeparam name="TInput1">映射源</typeparam>
    /// <typeparam name="TInput2">映射源</typeparam>
    /// <typeparam name="TOutput">映射目标</typeparam>
    public interface IMapper<in TInput1, in TInput2, out TOutput>
    {
        TOutput MapFrom(TInput1 input1, TInput2 input2);
    }

    /// <summary>
    /// 映射实体类型接口（扩展）
    /// </summary>
    /// <typeparam name="TInput1">映射源</typeparam>
    /// <typeparam name="TInput2">映射源</typeparam>
    /// <typeparam name="TInput3">映射源</typeparam>
    /// <typeparam name="TOutput">映射目标</typeparam>
    public interface IMapper<in TInput1, in TInput2, in TInput3, out TOutput>
    {
        TOutput MapFrom(TInput1 input1, TInput2 input2, TInput3 input3);
    }
}
