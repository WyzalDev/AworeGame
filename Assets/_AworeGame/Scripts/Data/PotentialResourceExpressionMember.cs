namespace _AworeGame.Scripts.Data
{
    public class PotentialResourceExpressionMember
    {
        public float ExpressionMember { get; set; }
        
        public PotentialResourceSource Source { get; set; }

        public PotentialResourceExpressionMember(PotentialResourceSource source, float expressionMember)
        {
            ExpressionMember = expressionMember;
            Source = source;
        }
    }
}