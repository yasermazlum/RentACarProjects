﻿using System.Threading;
using System.Threading.Tasks;
using Application.Features.Brands.Queries.GetList;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Xunit;
using static Application.Features.Brands.Queries.GetList.GetListBrandQuery;

namespace Application.Tests.Features.Brands.Queries.GetListBrand;

public class GetListBrandTests : BrandMockRepository
{
    private readonly GetListBrandQuery _query;
    private readonly GetListBrandQueryHandler _handler;

    public GetListBrandTests(BrandFakeData fakeData, GetListBrandQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetListBrandQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllBrandsShouldSuccessfuly()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 3 };
        GetListResponse<GetListBrandListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 2, result.Items.Count);
    }
}
