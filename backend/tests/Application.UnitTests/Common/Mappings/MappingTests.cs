using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using CleanArchitectureApi.Application.Common.Interfaces;
using CleanArchitectureApi.Application.Common.Models;
using CleanArchitectureApi.Application.ProductItems.Queries.GetProductItemsWithPagination;
using CleanArchitectureApi.Application.ProductLists.Queries.GetProducts;
using CleanArchitectureApi.Domain.Entities;
using NUnit.Framework;

namespace CleanArchitectureApi.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(ProductList), typeof(ProductListDto))]
    [TestCase(typeof(ProductItem), typeof(ProductItemDto))]
    [TestCase(typeof(ProductList), typeof(LookupDto))]
    [TestCase(typeof(ProductItem), typeof(LookupDto))]
    [TestCase(typeof(ProductItem), typeof(ProductItemBriefDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
