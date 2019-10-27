# JRWatermark Text Writer

|master|
|---|
|[![Build status](https://ci.appveyor.com/api/projects/status/wtc8t3hn7u1xc02d/branch/master?svg=true)](https://ci.appveyor.com/project/AngryShoes/watermarktextwriter/branch/master)|

- Used for write text as watermark in source image
- Can write watermark text in local and remote image files
  
`WriteWaterMarkTextAsync` signature manifest

| Parameters |            Function             |                                       Notes                                       |
| :--------: | :-----------------------------: | :-------------------------------------------------------------------------------: |
| originPath |        Origin image path        |
|  savePath  |    Generated image file path    |                                                                                   |
| waterText  | Watermark text display in image |                                                                                   |
|   color    |     Color of watermark text     |                         Type of **System.Drawing.Color**                          |
|   alpha    |     Control of transparency     | Less than 0 or greater than 255. The higher the value, the lower the transparency |
