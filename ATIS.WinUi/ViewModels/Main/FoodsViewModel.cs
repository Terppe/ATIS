﻿using ATIS.WinUi.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ATIS.WinUi.ViewModels.Main;
public class FoodsViewModel : ObservableObject //, INavigationAware
{
    #region [Private Data Members]
    private readonly IDataService _dataService;

    #endregion [Private Data Members]


    #region [Constructor]
    public FoodsViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }


    #endregion [Constructor]

}
