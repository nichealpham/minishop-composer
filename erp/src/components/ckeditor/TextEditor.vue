<template>
  <div class="text_editor">
    <div class="d-flex mb-3" v-if="isWebVersion && showButtons && false">
      <v-btn
        depressed
        class="button"
        style="width: 32%"
        @click="$refs.LinkPopup.open()"
      >
        <v-icon small class="mr-2"> mdi-link-variant-plus </v-icon>
        Link
      </v-btn>
      <v-spacer />
      <v-btn
        depressed
        class="button"
        style="width: 32%"
        :loading="uploading"
        @click="chooseFile"
      >
        <v-icon class="mr-2" small>mdi-cloud-upload-outline</v-icon>
        Upload
      </v-btn>
      <v-spacer />
      <v-btn
        depressed
        class="button"
        style="width: 32%"
        @click="$refs.Camera.start()"
      >
        <v-icon class="mr-2" small>mdi-camera-enhance-outline</v-icon>
        Camera
      </v-btn>
    </div>
    <ckeditor
      @ready="onEditorReady"
      :editor="editor"
      v-model="content"
      :config="config"
    ></ckeditor>
    <camera ref="Camera" @change="onCameraCapture" />
    <link-popup ref="LinkPopup" @change="onLinkChange" />

    <v-file-input
      id="select-file"
      style="display: none"
      @change="handleSelectFile()"
      accept="image/*"
      v-model="file"
    ></v-file-input>
  </div>
</template>

<script>
import CKEditor from "@ckeditor/ckeditor5-vue2";
import CustomEditor from "./ckeditor2";
import Camera from "./components.vue/Camera.vue";
import LinkPopup from "./components.vue/Link.vue";
import { firebaseUpload } from "@/plugins/firebase";

export default {
  components: {
    ckeditor: CKEditor.component,
    Camera,
    LinkPopup,
  },
  props: {
    value: {
      type: String,
      default: "",
    },
    showButtons: {
      type: Boolean,
      default: true,
    },
  },
  watch: {
    value(val) {
      if (this.editorReady) {
        this.content = val;
      } else {
        this.tmpContent = val;
      }
    },
    content(val) {
      this.$emit("input", val);
    },
  },
  mounted() {
    this.content = this.value || "";
  },
  data() {
    return {
      file: null,
      uploading: false,
      editor: CustomEditor,
      editorReady: false,
      content: "",
      tmpContent: "",
      config: {
        toolbar: [
          "imageUpload",
          "bold",
          "link",
          "|",
          "alignment:left",
          "alignment:center",
          "|",
          "bulletedList",
          "numberedList",
          "|",
          "fontBackgroundColor",
          "mediaEmbed",
          "insertTable",
          "|",
          "undo",
          "redo",
          "code",
          "codeBlock",
          "|",
          "underline",
          "fontSize",
        ],
        fillEmptyBlocks: false,
        language: "vn",
        mediaEmbed: {
          previewsInData: true,
        },
        extraPlugins: [createUploadAdapter],
      },
    };
  },
  methods: {
    chooseFile() {
      this.file = null;
      document.querySelector("#select-file").click();
    },
    async onEditorReady() {
      this.editorReady = true;
      this.content = this.tmpContent;
    },
    async handleSelectFile() {
      if (!this.file) {
        return;
      }
      var { name, type } = this.file;
      var path = `episode/${name}`;
      this.uploading = true;
      var url = await firebaseUpload(this.file, path);
      this.uploading = false;
      if (type.includes("image")) this.content += `<img src="${url}">`;
      else this.content += `<a href="${url}">${name}</a>`;
    },
    onCameraCapture(url) {
      this.content += `<img src="${url}">`;
    },
    onLinkChange({ name, url }) {
      this.content += `<a href="${url}">${name}</a>`;
    },
  },
};
function createUploadAdapter(editor) {
  editor.plugins.get("FileRepository").createUploadAdapter = (loader) => {
    return new UploadAdapter(loader);
  };
}

class UploadAdapter {
  constructor(loader) {
    this.loader = loader;
  }
  upload() {
    return this.loader.file.then(async (file) => {
      var fileName = `${Date.now()}-${file.name}`;
      var url = await firebaseUpload(file, fileName);
      return {
        default: url,
      };
    });
  }
  abort() {}
}
</script>

<style lang="scss">
.text_editor {
  .ck-editor__editable {
    min-height: 220px;
    border: 1px solid #bdbdbd !important;
    border-radius: 4px !important;
    margin-top: 2px !important;
    p {
      margin: 0px;
      padding: 0px;
    }
  }

  .v-btn {
    .v-btn__content {
      font-weight: 500;
      font-size: 0.75rem;
    }
  }
}
</style>
