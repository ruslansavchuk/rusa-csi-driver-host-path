// namespace Csi.HostPath.Node.Api.Utils;
//
// public class Mounter
// {
//     private const string defaultMountCommand = "mount";
//     const string netDev = "_netdev";
//
//     private readonly bool _trySystemd;
//     private readonly string _mounterPath;
//
//     public Mounter(string mounterPath)
//     {
//         _mounterPath = mounterPath;
//         _trySystemd = true;
//     }
//
//     public void Mount(string source, string target, string fsType, List<string> options, List<string> sensitiveOptions)
//     {
//         var mounterPath = "";
//         var (bind, bindOpts, bindRemountOpts, bindRemountOptsSensitive) =
//             MakeBindOptsSensitive(options, sensitiveOptions);
//         if (bind) {
//             DoMount(mounterPath, defaultMountCommand, source, target, fsType, bindOpts, bindRemountOptsSensitive, null /* mountFlags */, _trySystemd);
//             DoMount(mounterPath, defaultMountCommand, source, target, fsType, bindRemountOpts, bindRemountOptsSensitive,
//                 null /* mountFlags */, _trySystemd);
//         }
//         // // The list of filesystems that require containerized mounter on GCI image cluster
//         var fsTypesNeedMounter = new Dictionary<string, string>
//         {
//             {"nfs", null},
//             {"glusterfs", null},
//             {"ceph", null},
//             {"cifs", null},
//         };
//
//         if (fsTypesNeedMounter.ContainsKey(fsType))
//         {
//             mounterPath = _mounterPath;
//         }
//
//         DoMount(mounterPath, defaultMountCommand, source, target, fsType, options, sensitiveOptions,
//             null /* mountFlags */, _trySystemd);
//     }
//
//     private void DoMount(
//         string mounterPath, 
//         string mountCmd, 
//         string source, 
//         string target, 
//         string fstype, 
//         List<string> options, 
//         List<string> sensitiveOptions, 
//         List<string> mountFlags, 
//         bool systemdMountRequired)
//     {
//         throw new NotImplementedException();
// // 	mountArgs, mountArgsLogStr := MakeMountArgsSensitiveWithMountFlags(source, target, fstype, options, sensitiveOptions, mountFlags)
// // 	if len(mounterPath) > 0 {
// // 		mountArgs = append([]string{mountCmd}, mountArgs...)
// // 		mountArgsLogStr = mountCmd + " " + mountArgsLogStr
// // 		mountCmd = mounterPath
// // 	}
// //
// // 	if systemdMountRequired && mounter.hasSystemd() {
// // 		// Try to run mount via systemd-run --scope. This will escape the
// // 		// service where kubelet runs and any fuse daemons will be started in a
// // 		// specific scope. kubelet service than can be restarted without killing
// // 		// these fuse daemons.
// // 		//
// // 		// Complete command line (when mounterPath is not used):
// // 		// systemd-run --description=... --scope -- mount -t <type> <what> <where>
// // 		//
// // 		// Expected flow:
// // 		// * systemd-run creates a transient scope (=~ cgroup) and executes its
// // 		//   argument (/bin/mount) there.
// // 		// * mount does its job, forks a fuse daemon if necessary and finishes.
// // 		//   (systemd-run --scope finishes at this point, returning mount's exit
// // 		//   code and stdout/stderr - thats one of --scope benefits).
// // 		// * systemd keeps the fuse daemon running in the scope (i.e. in its own
// // 		//   cgroup) until the fuse daemon dies (another --scope benefit).
// // 		//   Kubelet service can be restarted and the fuse daemon survives.
// // 		// * When the fuse daemon dies (e.g. during unmount) systemd removes the
// // 		//   scope automatically.
// // 		//
// // 		// systemd-mount is not used because it's too new for older distros
// // 		// (CentOS 7, Debian Jessie).
// // 		mountCmd, mountArgs, mountArgsLogStr = AddSystemdScopeSensitive("systemd-run", target, mountCmd, mountArgs, mountArgsLogStr)
// // 		// } else {
// // 		// No systemd-run on the host (or we failed to check it), assume kubelet
// // 		// does not run as a systemd service.
// // 		// No code here, mountCmd and mountArgs are already populated.
// // 	}
// //
// // 	// Logging with sensitive mount options removed.
// // 	klog.V(4).Infof("Mounting cmd (%s) with arguments (%s)", mountCmd, mountArgsLogStr)
// // 	command := exec.Command(mountCmd, mountArgs...)
// // 	output, err := command.CombinedOutput()
// // 	if err != nil {
// // 		if err.Error() == errNoChildProcesses {
// // 			if command.ProcessState.Success() {
// // 				// We don't consider errNoChildProcesses an error if the process itself succeeded (see - k/k issue #103753).
// // 				return nil
// // 			}
// // 			// Rewrite err with the actual exit error of the process.
// // 			err = &exec.ExitError{ProcessState: command.ProcessState}
// // 		}
// // 		klog.Errorf("Mount failed: %v\nMounting command: %s\nMounting arguments: %s\nOutput: %s\n", err, mountCmd, mountArgsLogStr, string(output))
// // 		return fmt.Errorf("mount failed: %v\nMounting command: %s\nMounting arguments: %s\nOutput: %s",
// // 			err, mountCmd, mountArgsLogStr, string(output))
// // 	}
// // 	return err
// // }
//     }
//     
//     public void Unmount(string target)
//     {
//         throw new NotImplementedException();
//     }
//     
//     public (bool, List<string>, List<string>, List<string>) MakeBindOptsSensitive(List<string> options, List<string> sensitiveOptions) {
//         // Because we have an FD opened on the subpath bind mount, the "bind" option
//         // needs to be included, otherwise the mount target will error as busy if you
//         // remount as readonly.
//         //
//         // As a consequence, all read only bind mounts will no longer change the underlying
//         // volume mount to be read only.
//         var bindRemountOpts = new List<string> {"bind", "remount"};
//         var bindRemountSensitiveOpts = new List<string>();
//         var bind = false;
//         var bindOpts = new List<string> {"bind"};
//
//         // _netdev is a userspace mount option and does not automatically get added when
//         // bind mount is created and hence we must carry it over.
//         if (CheckForNetDev(options, sensitiveOptions)) {
//             bindOpts.Add(netDev);
//         }
//
//         foreach (var option in options)
//         {
//             switch (option)
//             {
//                 case "bind":
//                     bind = true;
//                     break;
//                 case "remount":
//                 default:
//                     bindRemountOpts.Add(option);
//                     break;
//             }
//         }
//         
//         foreach (var option in sensitiveOptions)
//         {
//             switch (option)
//             {
//                 case "bind":
//                     bind = true;
//                     break;
//                 case "remount":
//                 default:
//                     bindRemountSensitiveOpts.Add(option);
//                     break;
//             }
//         }
//
//         return (bind, bindOpts, bindRemountOpts, bindRemountSensitiveOpts);
//     }
//     
//     private bool CheckForNetDev(string[] options, string[] sensitiveOptions)
//     {
//         return options.Contains(netDev) || sensitiveOptions.Contains(netDev);
//     }
//     
//     
//     // func detectSafeNotMountedBehaviorWithExec(exec utilexec.Interface) bool {
//     //     // create a temp dir and try to umount it
//     //     path, err := os.MkdirTemp("", "kubelet-detect-safe-umount")
//     //     if err != nil {
//     //         klog.V(4).Infof("Cannot create temp dir to detect safe 'not mounted' behavior: %v", err)
//     //         return false
//     //     }
//     //     defer os.RemoveAll(path)
//     //     cmd := exec.Command("umount", path)
//     //     output, err := cmd.CombinedOutput()
//     //     if err != nil {
//     //         if strings.Contains(string(output), errNotMounted) {
//     //             klog.V(4).Infof("Detected umount with safe 'not mounted' behavior")
//     //             return true
//     //         }
//     //         klog.V(4).Infof("'umount %s' failed with: %v, output: %s", path, err, string(output))
//     //     }
//     //     klog.V(4).Infof("Detected umount with unsafe 'not mounted' behavior")
//     //     return false
//     // }
// }