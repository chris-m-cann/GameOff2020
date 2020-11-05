using System;

namespace Util
{
    public abstract class Either<TL, TR>
    {
        public abstract bool IsLeft { get; }
        public abstract bool IsRight { get; }
        public abstract void IfLeft(Action<TL> action);
        public abstract void IfRight(Action<TR> action);
        public abstract TResult Match<TResult>(Func<TL, TResult> ifLeft, Func<TR, TResult> ifRight);
        public abstract void Match(Action<TL> ifLeft, Action<TR> ifRight);


    }
    public class Left<TL, TR> : Either<TL, TR>
    {
        private readonly TL _value;

        public Left(TL left)
        {
            _value = left;
        }

        public override bool IsLeft => true;
        public override bool IsRight => false;

        public override void IfLeft(Action<TL> action)
        {
            action(_value);
        }

        public override void IfRight(Action<TR> action) { }

        public override TResult Match<TResult>(Func<TL, TResult> ifLeft, Func<TR, TResult> ifRight)
        {
            return ifLeft(_value);
        }

        public override void Match(Action<TL> ifLeft, Action<TR> ifRight)
        {
            ifLeft.Invoke(_value);
        }
    }

    public class Right<TL, TR> : Either<TL, TR>
    {
        private readonly TR _value;

        public Right(TR value)
        {
            _value = value;
        }

        public override bool IsLeft => false;
        public override bool IsRight => true;

        public override void IfLeft(Action<TL> action) { }

        public override void IfRight(Action<TR> action)
        {
            action(_value);
        }

        public override TResult Match<TResult>(Func<TL, TResult> ifLeft, Func<TR, TResult> ifRight)
        {
            return ifRight(_value);
        }

        public override void Match(Action<TL> ifLeft, Action<TR> ifRight)
        {
            ifRight.Invoke(_value);
        }
    }
}