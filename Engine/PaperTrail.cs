// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo.Engine
{
    /// <summary>
    /// Accumulates calculation history without UI coupling.
    /// The UI reads the TrailText property to update the display.
    /// This decoupling allows for easy unit testing of history formatting.
    /// </summary>
    public class PaperTrail : IPaperTrail
    {
        private string _pendingArgs = string.Empty;
        private string _trailText = string.Empty;

        /// <inheritdoc/>
        public string TrailText => _trailText;

        /// <inheritdoc/>
        public void AddArguments(string arguments)
        {
            _pendingArgs = arguments;
        }

        /// <inheritdoc/>
        public void AddResult(string result)
        {
            _trailText += _pendingArgs + " = " + result + "\n";
            _pendingArgs = string.Empty;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _trailText = string.Empty;
            _pendingArgs = string.Empty;
        }
    }
}
