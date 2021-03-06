﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace NRules.RuleModel
{
    /// <summary>
    /// Activation events that trigger the actions.
    /// </summary>
    [Flags]
    public enum ActionTrigger
    {
        /// <summary>
        /// Action is not triggered.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Action is triggered when activation is created.
        /// </summary>
        Activated = 0x1,

        /// <summary>
        /// Action is triggered when activation is updated.
        /// </summary>
        Reactivated = 0x2,

        /// <summary>
        /// Action is triggered when activation is removed.
        /// </summary>
        Deactivated = 0x4,
    }

    /// <summary>
    /// Action executed by the engine when the rule fires.
    /// </summary>
    [DebuggerDisplay("{Expression.ToString()}")]
    public class ActionElement : ExpressionElement
    {
        internal ActionElement(IEnumerable<Declaration> declarations, IEnumerable<Declaration> references, LambdaExpression expression, ActionTrigger actionTrigger)
            : base(declarations, references, expression)
        {
            ActionTrigger = actionTrigger;
        }

        /// <summary>
        /// Activation events that trigger this action.
        /// </summary>
        public ActionTrigger ActionTrigger { get; }
        
        internal override void Accept<TContext>(TContext context, RuleElementVisitor<TContext> visitor)
        {
            visitor.VisitAction(context, this);
        }
    }
}