using System.Collections.Generic;
using MIP.Interfaces;
using MIPLIB.EndPoints.Input;
using MIPLIB.EndPoints.Output;
using MIPLIB.States;

namespace MIPLIB.DSL
{
    public class test : SwitchingDirective
    {
        public test()
        {
            When().AButton("indegang").IsPushed().Turn().Light("zuukjesindekamer").OnOrOff(Rule);
            When().AButton("fsd").IsPushed().Turn().Light("asd").OnOrOff(Rule);
            When().ThisHappens(SunUp("sunsetsunrise")).Turn().Light("asdf").On(Rule);
            When().ThisHappens(SunUp("sunsetsunrise")).Turn().Light("asdf").Off(Rule);
        }
    }

    public class When
    {
        //listener
        public When()
        {
            
        }

        public Switch AButton(string identifier)
        {
            return new Switch(new List<IEndpointState>());
        }

        public Action ThisHappens(IInputEndpointActivator activator)
        {
            return new Action();
        }
    }

    public interface IInputEndpointActivator
    {
        InputEndpoint Endpoint { get; set; }
        IEndpointState State { get; set; }
    }
    
    public class InputEndpointActivator : IInputEndpointActivator
    {
        public InputEndpoint Endpoint { get; set; }
        public IEndpointState State { get; set; }
    }

    public class Activator
    {
        //activator
        public Activator()
        {
            
        }
    }

    public class Action
    {
        public Action()
        {
            
        }

        public Action Turn()
        {
            return this;
        }
    }

    public static class SwitchingDirectiveExtensions
    {
        public static Action IsPushed (this Switch activator)
        {
            return new Action ();
        }

        //public static Activator AButton (this When when, string buttonIdentifier)
        //{
        //    return new Activator ();
        //}

        public static Light Light (this Action action, string lightIdentifier)
        {
            return new Light(new List<IEndpointState>());
        }

        public static void OnOrOff (this Light outputEndpoint, IRule rule)
        {
            rule.FireWithInput(outputEndpoint);
        }

        public static void On (this OutputEndpoint outputEndpoint, IRule rule)
        {
            if (outputEndpoint.CurrentState is Off)
            {
                rule.FireWithInput(outputEndpoint);
            }
        }

        public static void Off (this OutputEndpoint outputEndpoint, IRule rule)
        {
            if (outputEndpoint.CurrentState is On)
            {
                rule.FireWithInput (outputEndpoint);
            }
        }
    }

    public abstract class SwitchingDirective
    {
        public When When()
        {
            return new When();
        }

        public IRule Rule { get; set; }

        public static InputEndpointActivator SunUp (string inputendpointIdentifier)
        {
            return new InputEndpointActivator
                       {

                       };
        }
    }
}
