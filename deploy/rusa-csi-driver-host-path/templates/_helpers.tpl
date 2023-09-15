{{/*
Csi controller service labels
*/}}
{{- define "csi-controller-service-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-controller-service
{{- end }}

{{/*
Csi controller labels
*/}}
{{- define "csi-controller-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-controller
{{- end }}

{{/*
Csi node plugion labels
*/}}
{{- define "csi-node-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-node
{{- end }}

{{/*
Csi storage class labels
*/}}
{{- define "csi-storage-class-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: storage-class
{{- end }}

{{/*
Csi driver labels
*/}}
{{- define "csi-driver-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-driver
{{- end }}

{{/*
General lables that should be attached to all objects
*/}}
{{- define "csi-common-labels" -}}
app.kubernetes.io/part-of: {{ include "csi-name" . }}
app.kubernetes.io/name: {{ include "csi-name" . }}
app.kubernetes.io/instance: {{ include "csi-name" . }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}

{{/*
Define plugin name
*/}}
{{- define "csi-name" -}}
{{- default "rusa.hostpath.csi.k8s.io" .Values.pluginName | quote }}
{{- end }}