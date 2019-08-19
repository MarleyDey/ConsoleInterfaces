# ConsoleInterfaces

## Option Menu

### Creating Option menu:
This will allow you to add the options dynamically to the option menu, it will then
be dsplayed with the custom title you can add in the display menu method
```csharp
var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.OPTIONS);
optionMenu.SetOptions("First Option", "Second Option", "Third Option");

optionMenu.DisplayMenu();
```
Output:
```
This is a menu title:
[1] First Option
[2] Second Option
[3] Third Option

Press [Enter] to select.
```

### Registering option select event:
This event will trigger when the option is selected, the event args will allow the
user to access the option number that was selected.
```csharp
static void Main(string[] args)
  {
     var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.OPTIONS);

     optionMenu.OptionSelect += OnOptionSelectEvent;

  }

 static void OnOptionSelectEvent(object sender, OptionsMenu.OptionSelectedEventArgs e)
  {
     //Event code here
  }
```

### Linking menus to options
This allows users to link menus to eachother so options can lead to other menus for
easy menu linking
```csharp
var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.OPTIONS);
optionMenu.SetOptions("An example option");

var secondOptionMenu = new OptionsMenu("This is a menu title", OptionsMenu.MenuType.OPTIONS);

optionMenu.SetOptionLink(1, secondOptionMenu); //This will open the second menu when the first option is selected
```

## Tick Box Menu

### Creating Tick Box menu:
This will allow you to add the box options dynamically to the box option menu, it
wiull then be displayed with empty tick boxs and allow the user to tick each.
```csharp
var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.TICKBOX);
optionMenu.SetOptions("First Option", "Second Option", "Third Option");

optionMenu.DisplayMenu();
```
Output: Option 1 and 3 has been selected in this example
```
This is a menu title:
[*] First Option
[ ] Second Option
[*] Third Option

Press [Space] to select.
Press [Enter] to select.
```

### Registering tick select event:
This event will trigger when the tick box is submitted, the event args will allow 
the user to access the options that were selected.
```csharp
static void Main(string[] args)
   {
       var optionMenu = new OptionsMenu("This is a menu title:", OptionsMenu.MenuType.TICKBOX);

       optionMenu.BoxOptionSelect += OnBoxsSelectEvent;
   }

static void OnBoxsSelectEvent(object sender, OptionsMenu.BoxOptionSelectedEventArgs e)
   {
       Console.WriteLine($"Options selected: {e.options.Count}");
   }
```
