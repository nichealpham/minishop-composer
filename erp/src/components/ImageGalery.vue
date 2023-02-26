<template>
  <v-row class="image_galery">
    <v-col
      v-for="(item, n) in images"
      :key="n"
      class="d-flex child-flex ma-0 pa-2"
      cols="3"
    >
      <div class="image_item_container">
        <v-img
          :src="item.attachmentUrl"
          :lazy-src="item.attachmentUrl"
          aspect-ratio="1"
          class="grey lighten-2"
          @click="showImage(item.attachmentUrl)"
        >
          <template v-slot:placeholder>
            <v-row class="fill-height ma-0" align="center" justify="center">
              <v-progress-circular
                indeterminate
                color="grey lighten-5"
              ></v-progress-circular>
            </v-row>
          </template>
        </v-img>
        <v-btn
          fab
          x-small
          class="image_btn"
          @click="detach(item.attachmentID)"
          :loading="item.isDetaching"
          elevation="1"
        >
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </div>
    </v-col>

    <v-col class="d-flex child-flex ma-0 pa-2" cols="3">
      <v-row
        class="fill-height ma-0 pa-0 upload_container"
        align="center"
        justify="center"
      >
        <v-btn
          plain
          class="pa-2 text-capitalize"
          height="100%"
          width="100%"
          style="min-height: 108px"
          :loading="loading"
          @click="chooseFile"
        >
          <v-icon>mdi-upload</v-icon>
          {{ $t("common.upload") }}
        </v-btn>
        <v-file-input
          :id="`select-file-galery-${fileInputID}`"
          style="display: none; opacity: 0"
          @change="uploadFiles()"
          accept="image/*"
          v-model="files"
          truncate-length="15"
          multiple
        ></v-file-input>
      </v-row>
    </v-col>

    <v-dialog v-model="showModal" max-width="1020px">
      <v-card>
        <v-img
          :src="targetImageUrl"
          :lazy-src="targetImageUrl"
          class="grey lighten-2"
          width="100%"
        />
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
import { TargetItemType, AttachmentType } from "@/plugins/contants";
import { firebaseUpload } from "@/plugins/firebase";
import { awaitAll } from "@/plugins/helpers";

export default {
  props: {
    targetItemID: {
      type: String,
      default: null,
    },
    targetItemType: {
      type: Number,
      default: TargetItemType.Asset,
    },
  },
  components: {},
  data() {
    return {
      images: [],
      files: [],
      loading: false,
      targetImageUrl: "",
      showModal: false,
      fileInputID: Math.floor(Math.random() * 1000),
    };
  },
  created() {
    this.render();
  },
  methods: {
    async render() {
      if (!this.$clinicID) return;
      if (!this.targetItemID || !this.targetItemType) return;
      this.loading = true;
      var query = {
        attachmentType: AttachmentType.Image,
        targetItemType: this.targetItemType,
      };
      var headers = { page: 1, limit: 20 };
      var { items, error } = await this.$httpClient.get(
        `/owner/clinics/${this.$clinicID}/targetitems/${this.targetItemID}/attachments`,
        query,
        headers
      );
      this.loading = false;
      if (error) {
        return this.showError(
          `Cannot get images galery! Please try again later`
        );
      }
      this.images = items;
    },
    async detach(attachmentID) {
      if (!attachmentID || !this.targetItemID || !this.targetItemType) return;

      var ind = this.images.findIndex((i) => i.attachmentID == attachmentID);
      if (ind == -1) return;
      this.images[ind].isDetaching = true;
      this.$forceUpdate();

      var result = await this.$httpClient.delete(
        `/owner/clinics/${this.$clinicID}/targetitems/${this.targetItemID}/attachments/${attachmentID}`
      );
      if (!result || result.error) {
        return this.showError(
          `Cannot delete image item! Please try again later`
        );
      }
      this.images.splice(ind, 1);
    },
    async uploadFiles() {
      var queues = [];
      this.loading = true;
      for (var file of this.files) {
        queues.push(this.uploadSingleFile(file));
      }
      var images = await awaitAll(queues);
      this.images = this.images.concat(images.filter((i) => i.attachmentUrl));
      this.loading = false;
    },
    async uploadSingleFile(file) {
      if (!file) {
        return;
      }
      var { name } = file;
      var path = `galeries/${this.targetItemID}/${name}`;
      var downloadUrl = await firebaseUpload(file, path);
      var query = {
        targetItemType: this.targetItemType,
      };
      var body = {
        attachmentUrl: downloadUrl,
        attachmentName: name,
        attachmentType: AttachmentType.Image,
      };
      return await this.$httpClient.post(
        `/owner/clinics/${this.$clinicID}/targetitems/${this.targetItemID}/attachments`,
        query,
        body
      );
    },
    chooseFile() {
      document.querySelector(`#select-file-galery-${this.fileInputID}`).click();
    },
    showImage(url) {
      this.targetImageUrl = url;
      this.showModal = true;
    },
    clean() {
      this.images = [];
      this.files = [];
      this.loading = false;
    },
  },
};
</script>

<style lang="scss">
.image_galery {
  .image_item_container {
    position: relative;
    .v-image {
      border-radius: 4px;
      overflow: hidden;
      cursor: pointer;
    }

    .image_btn {
      position: absolute;
      top: 0px;
      margin-top: -10px;
      margin-left: -10px;
    }
    .v-progress-linear {
      margin-top: -8px;
    }
  }
  .upload_container {
    border-radius: 8px;
    overflow: hidden;
    border: 2px dashed #757575;
    background-color: #f5f5f5;
  }
}
</style>
