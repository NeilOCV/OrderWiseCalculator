using System;

namespace OrderWiseCalculator
{
    public class MathGeniusEventArgs : EventArgs
    {
        public decimal Value { get; set; }
    }
    public class ErrorArgs : EventArgs
    {
        public string ErrorMessage { get; set; }
    }
    public class MathsGenius
    {
        public delegate void GeniusHasResultsEventsHandler(object source, MathGeniusEventArgs args);
        public event GeniusHasResultsEventsHandler GeniusHasResults;
        public delegate void ErrorsHandler(object source, ErrorArgs args);
        public event ErrorsHandler GeniusEncounteredErrors;

        private Operators _previousOperator;
        private decimal _numberInBuffer = 0;
        private Operators _operator;
        public decimal Number { get; set; }
        public Operators Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                if ((value == Operators.Equals) || (value == Operators.Clear))
                {
                    _operator = value;
                    switch (value)
                    {
                        case Operators.Clear:
                            {
                                _numberInBuffer = 0;
                                OnGeniusHasResults(_numberInBuffer);
                            }
                            break;
                        case Operators.Equals:
                            {
                                try
                                {



                                    switch (_previousOperator)
                                    {

                                        case Operators.Plus:
                                            {
                                                _numberInBuffer += Number;
                                                Number = 0;
                                            }
                                            break;
                                        case Operators.Minus:
                                            {
                                                _numberInBuffer -= Number;
                                                Number = 0;
                                            }
                                            break;
                                        case Operators.Multiply:
                                            {
                                                _numberInBuffer *= Number;
                                                Number = 0;
                                            }
                                            break;
                                        case Operators.Divide:
                                            {
                                                _numberInBuffer /= Number;
                                                Number = 0;
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    OnGeniusEncounteredErrors(ex.Message);
                                }
                                OnGeniusHasResults(_numberInBuffer);
                                _numberInBuffer = 0;
                            }
                            break;

                    }
                }
                else
                {
                    _previousOperator = value;
                    switch (_previousOperator)
                    {

                        case Operators.Plus:
                            {
                                _numberInBuffer += Number;
                                Number = 0;
                            }
                            break;
                        case Operators.Minus:
                            {
                                if (_numberInBuffer == 0)
                                    _numberInBuffer = Number;
                                else
                                    _numberInBuffer -= Number;
                                Number = 0;
                            }
                            break;
                        case Operators.Multiply:
                            {
                                if (_numberInBuffer > 0)
                                    _numberInBuffer *= Number;
                                else
                                    _numberInBuffer = Number;
                                Number = 0;
                            }
                            break;
                        case Operators.Divide:
                            {
                                if (_numberInBuffer > 0)
                                    _numberInBuffer /= Number;
                                else
                                    _numberInBuffer = Number;
                                Number = 0;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        protected virtual void OnGeniusEncounteredErrors(string strError)
        {
            if (GeniusEncounteredErrors != null)
                GeniusEncounteredErrors(this, new ErrorArgs() { ErrorMessage = strError });
        }
        protected virtual void OnGeniusHasResults(decimal dblValue)
        {
            if (GeniusHasResults != null)
                GeniusHasResults(this, new MathGeniusEventArgs() { Value = dblValue });
        }

    }
}
