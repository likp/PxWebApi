﻿using PCAxis.Paxiom;
using PxWeb.Api2.Server.Models;

namespace PxWeb.Code.Api2.DataSelection
{
    public interface ISelectionHandler
    {
        public Selection[]? GetSelection(IPXModelBuilder builder, PXModel model, VariablesSelection? variablesSelection, out Problem? problem);
    }
}
