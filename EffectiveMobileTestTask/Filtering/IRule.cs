namespace EffectiveMobileTestTask.Filtering;

public interface IRule<T>
{
    bool Should(T value);
}