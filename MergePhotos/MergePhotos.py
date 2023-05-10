from PIL import Image, ImageEnhance

# Open images and store them in a list

im = Image.open('img2.jpg').convert('RGBA')
alpha = im.split()[3]
alpha = ImageEnhance.Brightness(alpha).enhance(.5)
im.putalpha(alpha)


images = [Image.open(x) for x in ['img1.jpg']]
total_width = 0
max_height = 0


# find the width and height of the final image
for img in images:
    total_width += img.size[0]
    max_height = max(max_height, img.size[1])
current_width = 0
# create a new image with the appropriate height and width
new_img = Image.new('RGB', (total_width, max_height))
new_img.paste(im, (current_width,0))
current_width += im.size[0]
# Write the contents of the new image

for img in images:
  new_img.paste(img, (current_width,0))
  current_width += img.size[0]

# Save the image
new_img.save('NewImage.jpg')