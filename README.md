# ConsoleInterfaces

## Option Menu

### Creating Option menu:
This will allow you to add the options dynamically to the option menu, it will then
be dsplayed with the custom title you can add in the display menu method
```csharp
OptionsMenu optionsMenu = new OptionsMenu();

optionsMenu.SetOptions("This is the first option", "A second option", "And a third");
optionsMenu.DisplayMenu("This is a title:");
```
Output:
```
This is a title:
[1] This is the first option
[2] A second option
[3] And a third

Press [Enter] to select.
```


### Registering option select event:
This event will trigger when the option is selected, the event args will allow the
user to access the option number that was selected.
```csharp
 OptionsMenu optionsMenu = new OptionsMenu();

optionsMenu.OptionSelect += OnOptionSelect;
```
```csharp
static void OnOptionSelect(object sender, OptionsMenu.OptionSelectedEventArgs e)
{
     Console.WriteLine("You have selected option " + e.option);
}
```

## Tick Box Menu

### Creating Tick Box menu:
This will allow you to add the box options dynamically to the box option menu, it
wiull then be displayed with empty tick boxs and allow the user to tick each.
```csharp
TickBoxMenu tickBoxMenu = new TickBoxMenu();

tickBoxMenu.SetBoxOptions("This is the first tick box", "The second tick box", "And the third");
tickBoxMenu.DisplayMenu("This is a tick box title:");
```
Output: Option 1 has been selected in this example
```
This is a tick box title:
[*] This is the first tick box
[ ] The second tick box
[ ] And the third

Press [Space] to select.
Press [Enter] to continue.
```

### Registering tick select event:
This event will trigger when the tick box is submitted, the event args will allow 
the user to access the options that were selected.
```csharp
TickBoxMenu tickBoxMenu = new TickBoxMenu();

tickBoxMenu.BoxOptionSelect += OnBoxOptionSelect;
```
```csharp
static void OnBoxOptionSelect(object sender, TickBoxMenu.BoxOptionSelectedEventArgs e)
  {
      Console.WriteLine("Options selected:");
      foreach (int option in e.options)
      {
          Console.WriteLine("- " + option);
      }
  }
```
