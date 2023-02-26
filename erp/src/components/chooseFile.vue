<template>
  <div class="w-100 mt-1" style="text-align:center;">
    <v-file-input
      id="select-file"
      style="display: none;opacity: 0"
      @change="handleFileUpload()"
      accept="image/*"
      v-model="file"
      truncate-length="15"
    ></v-file-input>
    <v-btn
      class="mb-3 btn-upload text-capitalize"
      @click="chooseFile"
      :loading="uploading"
      small
      elevation="0"
    >
      {{ $t("common.selectfile") }}
    </v-btn>
  </div>
</template>

<script>
import { firebaseUpload } from "@/plugins/firebase";

export default {
  props: ["value"],
  data() {
    return {
      uploading: false,
      file: null,
    };
  },
  methods: {
    chooseFile() {
      document.querySelector("#select-file").click();
    },
    async handleFileUpload() {
      if (!this.file) {
        return;
      }
      var { name } = this.file;
      var path = `file/${name}`;
      this.uploading = true;
      var downloadUrl = await firebaseUpload(this.file, path);
      this.uploading = false;
      this.$emit("input", downloadUrl);
    },
  },
};
</script>
