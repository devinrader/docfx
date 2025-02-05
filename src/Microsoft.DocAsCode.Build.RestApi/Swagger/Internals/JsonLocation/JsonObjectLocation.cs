﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;

namespace Microsoft.DocAsCode.Build.RestApi.Swagger.Internals;

internal class JsonObjectLocation : IJsonLocation
{
    private readonly string _propertyName;

    public JsonObjectLocation(string propertyName)
    {
        _propertyName = propertyName;
    }

    public void WriteTo(StringBuilder sb)
    {
        sb.Append('/');
        sb.Append(RestApiHelper.FormatDefinitionSinglePath(_propertyName));
    }
}
