# dockerfiles

This directory contains a sub-directory for each image type that is used in the project.

>**Important notes:**
>* All images are built with context set to repository's root directory. This allows for access of shared files, such as certs, without duplication. It also enables sharing outputs between images, if needed.