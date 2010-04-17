using System;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using HtmlTags;
using Kokugen.Web.Actions.Project.Manage.Users.Add;
using Kokugen.Web.Actions.Project.Manage.Users.Delete;
using Kokugen.Web.Conventions;

namespace Kokugen.Web.Actions.Project.Manage.Menu
{
    public class ManageProjectMenuAction
    {
        private readonly IUrlRegistry _urlRegistry;

        public ManageProjectMenuAction(IUrlRegistry urlRegistry)
        {
            _urlRegistry = urlRegistry;
        }

        [FubuPartial]
        public HtmlTag Execute(ManageProjectMenuRequest request)
        {
            var menus = new HtmlTag("ul");

            menus.Child(new HtmlTag("li").Child(
             new LinkTag("Remove User",
                         _urlRegistry.UrlFor(new DeleteProjectUserRequest() { Id = request.Id }),
                         "manage-proj-menu").Title("Remove User")).AddClass("bar"));

            menus.Child(new HtmlTag("li").Child(
                new LinkTag("Add User",
                            _urlRegistry.UrlFor(new AddUserToProjectRequest() {Id = request.Id}),
                            "manage-proj-menu").Title("Add User")).AddClass("bar"));

            var script = new HtmlTag("script").Attr("type", "text/javascript")
                .Text(
                    "$(function(){$('.manage-proj-menu').ajaxDialog({onComplete:HandleAjaxResponse,dataType:'json'});});");

            menus.Next = script;
            return menus;
        }
    }

    public class ManageProjectMenuRequest : IRequestById
    {
        public Guid Id { get; set; }
    }


}