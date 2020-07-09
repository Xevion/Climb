from PIL import Image
from colorsys import rgb_to_hsv
from math import ceil

img = Image.open('gradient.png')
pix = img.load()
print(img.size)

def takespread(sequence, num):
    length = float(len(sequence))
    arr = []
    for i in range(num):
    	arr.append(sequence[round(length * (i / num))])
    return arr

pixels = [pix[0, y] for y in range(0, img.size[1])]
pixels = [[round(p / 255, 3) for p in pixel] for pixel in pixels]
# pixels = [f'[{str(i).rjust(3)}] ' + ' '.join(str(p).rjust(3) for p in pixel) for i, pixel in enumerate(pixels)]
out = []
for pixel in takespread(pixels, 250):
	out.append(f'float4({pixel[0]}, {pixel[1]}, {pixel[2]}, 1.0),')
print('\n'.join(out))