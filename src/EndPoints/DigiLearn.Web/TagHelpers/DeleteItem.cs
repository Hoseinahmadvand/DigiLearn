using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DigiLearn.Web.TagHelpers;

public class DeleteItem : TagHelper
{

    public string Url { get; set; }
    public string Description { get; set; } = "";
    public string Class { get; set; } = "bg-red-500 text-white px-4 py-2  rounded-full  hover:bg-red-600";
    public bool IsButtonTag { get; set; } = true;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (IsButtonTag)
        {
            output.TagName = "button";
        }
        else
        {
            output.TagName = "a";
        }
        output.Attributes.Add("onClick", $"deleteItem('{Url}','{Description}')");
        output.Attributes.Add("class", Class);
        base.Process(context, output);
    }

}