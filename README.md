# CSVReader
Petite classe pour aider à manipuler des fichier CSV en utilisant des model

# Utilisation

Pour le moment la class ne gère que les models contenant des propriété de type `string`

## Lecture d'un fichier CSV

Il faut tout dabord créer un model, par exemple:

```csharp
public class Fruit
{
    public string Nom { get; set; }
    public string Provenance { get; set; }
    public string Prix { get; set; }
    public string Quantite { get; set; }
    public string Inutile { get; set; }
}
```

Puis lorsque vous créé votre objet `CSVFileReader` procéder comme ceci:

```csharp
CSVFileReader<Fruit> myFile = new CSVFileReader<Fruit>(pathToTheFile);
```

Notez toutefois que si il n'y a pas de fichier à cet endroit, un fichier va y être créé avec à l'intérieur des headers correspondans aux propriétés du model 

Ensuite, il n'y a plus qu'a récupérer la liste de model contneu dans le fichier CSV via la fonction :

```csharp
List<Fruit> fruits = new List<Fruit>();
fruits = myFile.GetData();
```

Et voilà, vous avez récupéré une liste de de votre model que vous pouvez utiliser comme bon vous semble.

## Ecriture d'un fichier CSV

Vous devez créer un objet de type `CSVFileReader` comme pour la lecture.

Vous pouvez ajouter des infos à votre fichier avec la fonction:

```csharp
Fruit mangue = new Fruit() { Nom = "Mangue", Prix = "150", Provenance = "Sud-est", Quantite = "90Kg",Inutile="Hello there" };
myFile.AddData(mangue);
```

Vous pouvez un model ou une liste de votre model.

## Queslques fonctions

### `GetData()`
```csharp
public List<T> GetData()
```

Renvoi une liste contenant toutes les infos dans votre fichier CSV sous la forme d'une list du model passé dans le constructeur

----
### `AddData()`

```csharp
public void AddData(T model)
```
```csharp
public void AddData(List<T> models)
```

Permet d'ajouter une ou plusieurs ligne à votre fichier CSV

----

### `WriteData()` 

```csharp
public void WriteData(T model)
```
```csharp
public void WriteData(List<T> model)
```

Ecrase les donnée dans le fichier avec la ou les nouvelles données


