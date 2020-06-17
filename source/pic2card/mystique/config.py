"""
Gloabal settings and constants.
"""
import os

# max 2mb
IMG_MAX_UPLOAD_SIZE = 2e+6

# tf-serving url
TF_SERVING_URL = os.environ.get("TF_SERVING_URL",
                                "http://172.17.0.5:8501")
TF_SERVING_MODEL_NAME = "mystique"
ENABLE_TF_SERVING = os.environ.get("ENABLE_TF_SERVING",
                                   False)
# image object's max size and resizing factor
IMG_OBJECT_MAX_SIZE = 1000
# Image resizing factor for custom image pipeline is decided by the nth power of
# 10 of the IMG_OBJECT_MAX_SIZE
IMG_OBJECT_RESIZE_FACTOR = 0.1 * str(IMG_OBJECT_MAX_SIZE).count('0')
# image hosting max size and default image url
IMG_MAX_HOSTING_SIZE = 1000000
DEFAULT_IMG_HOSTING = "https://lh3.googleusercontent.com/-snm-WznsB3k/XrAWKVCBC3I/AAAAAAAAB8Y/tR-2f8CzboQCmyTzrAfj9Xtvnbeh9PJ8QCK8BGAsYHg/s0/2020-05-04.png"
