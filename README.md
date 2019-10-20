# JRWatermark Text Writer

- Used for write text as watermark in source image
- Can write watermark text in local and remote image files

| Parameters |            Function             |                                      Comment                                      |
| :--------: | :-----------------------------: | :-------------------------------------------------------------------------------: |
| originPath |        Origin image path        |
|  savePath  |    Generated image file path    |                                                                                   |
| waterText  | Watermark text display in image |                                                                                   |
|   color    |     Color of watermark text     |                         Type of **System.Drawing.Color**                          |
|   alpha    |     Control of transparency     | Less than 0 or greater than 255. The higher the value, the lower the transparency |
