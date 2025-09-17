using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;

namespace Samples.Client.BlazorWasm.Pages
{
    public partial class Home
    {
        [CascadingParameter] public string? LastMessage { get; set; }
        [CascadingParameter] public ImmutableList<string>? Messages { get; set; }
    }
}