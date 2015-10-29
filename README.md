# XPShadow

shadow for uwp controls

Display
--------
####button
![](https://raw.githubusercontent.com/brookshi/XPShadow/master/shadow_button.png)

#####card in list view
![](https://raw.githubusercontent.com/brookshi/XPShadow/master/shadow_cardview.png)

Usage
--------
``` java
 <xp:Shadow 
    Z_Depth="1"        //shadow depth, from 1 to 5
    CornerRadius="10"> // shadow corner radius, 0-1 for percent of width, > 1 for actual value
      <Border Width="80" Height="80" Background="White" CornerRadius="10"/>
  </xp:Shadow>
```

License
--------
``` 
Copyright 2015 Brook Shi

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License. 
```
