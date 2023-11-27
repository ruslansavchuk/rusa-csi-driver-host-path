# CSI Hostpath Driver

I use volume name as volume id because i know 

This repository contains the CSI hostpath driver.
The driver can be easilly deployed using helm chart published together with the release.

The CSI driver implementation splited into several projects
- controller
- node

Controller implements CSI api related to the identity and controller.
Node Implements CSI api related to the identoty and node.

This driver based on the idea of creating the separate folder for storign all the volumes as subfolders inside
and mounting that folder to the folder created for container using bind mount functionality of linux

In current implementation the only configurable things:
node:
- node data directory

controoler:
- controller state directory