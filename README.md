# GGJ17
The repository for the GGJ17. This won't be touched until the GGJ starts.

## Code Styleguide

1. Pascalcase for everything public or internal, Camelcase for everything private or protected (except for the Unity methods which need to be Uppercase)
2. Classes and Namespaces are always Pascalcase
3. Parameters are always Camelcase
4. Constants are Uppercase and Underscore
5. Never use Underscore ever (except for case 4)
6. GetComponent<>() only in Start, Awake, OnTriggerEnter, OnCollisionEnter, OnTriggerExit, OnCollisionExit or when it get called once over time.
7. Instead of tag == "" use CompareTag()
8. Curly bracets are always on a new line.
9. Never expose fields, always wrap them in Parameters if they need to be accessed externally. For the Unity Inspector use the [SerializeField]-attribute. Use [Range] over fields that may be modified externally.
10. If you want to extend the editor, talk to your team first about it.
11. Try not to use the Singleton pattern. Please.
12. A lot of small generic components > a few big specific ones.
13. Use the Unity Events whenever possible.
14. Any questions? You know where I sit. :)
15. If anything fails: [MSDN](https://msdn.microsoft.com/en-us/library/ff926074.aspx)

### Example

```C#
public class Foo : MonoBehaviour
{
    new Renderer renderer;

    protected int bar;
    
    public int Bar
    {
        get { return bar; }
        set { bar = value; }
    }
    
    void Start()
    {
        bar = 2;
        renderer = GetComponent<Renderer>();
    }
    
    public void AddToBar(int bar)
    {
        this.bar += bar;
    }
}
```
