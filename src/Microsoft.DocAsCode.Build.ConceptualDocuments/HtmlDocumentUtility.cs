﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net;
using HtmlAgilityPack;

namespace Microsoft.DocAsCode.Build.ConceptualDocuments;

public static class HtmlDocumentUtility
{
    public static SeparatedHtmlInfo SeparateHtml(string contentHtml)
    {
        if (contentHtml == null)
        {
            throw new ArgumentNullException();
        }
        var content = new SeparatedHtmlInfo();

        var document = new HtmlDocument();
        document.LoadHtml(contentHtml);

        // TODO: how to get TITLE
        // InnerText in HtmlAgilityPack is not decoded, should be a bug
        var headerNode = document.DocumentNode.SelectSingleNode("//h1|//h2|//h3");
        content.Title = WebUtility.HtmlDecode(headerNode?.InnerText);

        if (headerNode != null && GetFirstNoneCommentChild(document.DocumentNode) == headerNode)
        {
            content.RawTitle = headerNode.OuterHtml;
            headerNode.Remove();
        }
        else
        {
            content.RawTitle = string.Empty;
        }

        content.Content = document.DocumentNode.OuterHtml;

        return content;
    }

    private static HtmlNode GetFirstNoneCommentChild(HtmlNode node)
    {
        var result = node.FirstChild;
        while (result != null && (result.NodeType == HtmlNodeType.Comment || string.IsNullOrWhiteSpace(result.OuterHtml)))
        {
            result = result.NextSibling;
        }
        return result;
    }
}
